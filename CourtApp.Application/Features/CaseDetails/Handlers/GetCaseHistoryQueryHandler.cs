using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CourtApp.Application.Features.CaseDocuments.Services;
using CourtApp.Application.Features.CaseDetails.Dtos;
using CourtApp.Application.Features.CaseDetails.Queries;

namespace CourtApp.Application.Features.CaseDetails.Handlers
{

    public class GetCaseHistoryQueryHandler : IRequestHandler<GetCaseHistoryQuery, Result<CaseHistoryDto>>
    {
        private readonly IUserCaseRepository _CaseRepo;
        private readonly IWorkMasterRepository _WorkRepo;
        private readonly IWorkMasterSubRepository _WorkSRepo;
        private readonly ICaseProceedingRepository _ProceedingRepo;
        private readonly IMapper _mapper;
        private readonly ICaseDocsRepository _CaseDocRepo;
        public GetCaseHistoryQueryHandler(IUserCaseRepository _CaseRepo,
            IMapper _mapper,
            IWorkMasterRepository _WorkRepo,
            ICaseProceedingRepository _ProceedingRepo,
            ICaseDocsRepository caseDocRepo,
            IWorkMasterSubRepository _WorkSRepo)
        {
            this._CaseRepo = _CaseRepo;
            this._mapper = _mapper;
            this._WorkRepo = _WorkRepo;
            this._ProceedingRepo = _ProceedingRepo;
            _CaseDocRepo = caseDocRepo;
            this._WorkSRepo = _WorkSRepo;

        }
        public async Task<Result<CaseHistoryDto>> Handle(GetCaseHistoryQuery request, CancellationToken cancellationToken)
        {
            var caseDetail = await _CaseRepo.GetByIdAsync(request.CaseId);

            if (caseDetail == null)
                return await Result<CaseHistoryDto>.FailAsync("Case not found");

            var chr = _mapper.Map<CaseHistoryDto>(caseDetail);

            var cprocs = await _ProceedingRepo.GetProceedingByCaseIdAsync(request.CaseId);

            var PWorks = cprocs.GroupBy(pd => pd.ProceedingDate)
                .Select(g => new
                {
                    ProceedingDate = g.Key,
                    works = g.Select(s => s.ProcWork)
                            .SelectMany(w => w.Works)
                            .ToList()
                }).ToList();

            var result = PWorks.Select(pw =>
            {
                
                var WDetails = pw.works
                    .Where(w => w.WorkId != Guid.Empty)
                    .GroupBy(w => w.WorkId)
                    .ToDictionary(
                        g => g.Key,
                        g => new
                        {
                            g.First().Status,
                            g.First().AppliedOn,
                            g.First().ReceivedOn,
                            g.First().WorkTypeId
                        });

                var workDetails = _WorkSRepo.Entities
                    .Where(w => pw.works.Select(x => x.WorkId).Contains(w.Id)) // Filter only matching work IDs
                    .Select(w => new
                    {
                        WorkID = w.Id,
                        WorkName = w.Name,
                        WorkType = w.Work.Name,
                        WorkStatus = WDetails.ContainsKey(w.Id) ? WDetails[w.Id].Status : 0,
                        WorkDoneDate = WDetails.ContainsKey(w.Id)
                            ? (WDetails[w.Id].Status == 1
                                ? WDetails[w.Id].AppliedOn.ToString("dd/MM/yyyy")
                                : (WDetails[w.Id].Status == 2 ? WDetails[w.Id].ReceivedOn.ToString("dd/MM/yyyy") : null))
                            : null,
                        AppliedOn = WDetails.ContainsKey(w.Id)
                                ? (WDetails[w.Id].Status == 2 ? WDetails[w.Id].AppliedOn.ToString("dd/MM/yyyy") : null) : null
                    })
                    .ToList();

                return new
                {
                    pw.ProceedingDate,
                    WorkDetails = workDetails
                };
            }).ToList();

            var cprocdt = cprocs
                .Where(w => w.CaseId == request.CaseId)
                .Select(s => new CaseActivityDto
                {
                    NextDate = s.NextDate?.ToString("dd/MM/yyyy") ?? "",
                    Stage = s.StageId != null ? s.Stage.Name : "",
                    Activity = s.SubHead.Name,
                    Type = s.Head.Name,
                    Date = (s.ProceedingDate ?? s.CreatedOn),
                    WorkDetail = s.ProcWork != null ? new List<CaseWorkDetailDto>
                    {
                            new CaseWorkDetailDto
                            {
                                WorkingDate = s.ProcWork.LastWorkingDate?.ToString("dd/MM/yyyy") ??"",
                                Works = result
                                    .Where(wd => wd.ProceedingDate == s.ProceedingDate) // Match by ProceedingDate
                                    .SelectMany(wd => wd.WorkDetails)
                                    .Select(w => new CaseWorkDto
                                    {
                                        WorkType = w.WorkType,
                                        Work = w.WorkName,
                                        Status=w.WorkStatus==1?"Work Done":w.WorkStatus==2?"Copy Recieved":"",
                                        Date=w.WorkDoneDate,
                                        AppliedOn=w.AppliedOn
                                    })
                                    .ToList()
                            }
                    }
                    : new List<CaseWorkDetailDto>()
                })
                .ToList();

            var Docs = _CaseDocRepo
                .Entities
                .Include(d => d.DO)
                .Where(w => w.CaseId == request.CaseId).Select(s => new CaseDocumentDto
                {
                    Id = s.Id,
                    DocType = s.DOTypeId == 1 ? "Drafting" : "Order",
                    DocFilePath = s.Path,
                    DocName = s.DO.Name_En.ToUpper(),
                    DocDate = s.DocDate.ToString("dd/MM/yyyy")
                }).ToList();

            chr.History = cprocdt;
            chr.Docs = Docs;
            return Result<CaseHistoryDto>.Success(chr);
        }
    }
}
