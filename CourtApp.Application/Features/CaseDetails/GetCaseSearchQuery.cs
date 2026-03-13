using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.DTOs.CaseDetails;
using CourtApp.Application.Extensions;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseDetails
{
    public class GetCaseSearchQuery : IRequest<PaginatedResult<GetCaseSearchResponse>>
    {
        public string Type { get; set; }
        public string Value { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string UserId { get; set; }
    }
    public class GetCaseSearchQueryHandler : IRequestHandler<GetCaseSearchQuery, PaginatedResult<GetCaseSearchResponse>>
    {
        private readonly IUserCaseRepository _CaseRepo;
        private readonly IMapper _mapper;
        private readonly ICaseDocsRepository _CaseDocRepo;
        public GetCaseSearchQueryHandler(IUserCaseRepository _CaseRepo, IMapper _mapper, ICaseDocsRepository _CaseDocRepo)
        {
            this._CaseRepo = _CaseRepo;
            this._CaseDocRepo = _CaseDocRepo;
            this._mapper = _mapper;
        }
        public async Task<PaginatedResult<GetCaseSearchResponse>> Handle(GetCaseSearchQuery request, CancellationToken cancellationToken)
        {
            IQueryable<GetCaseSearchResponse> cases = new List<GetCaseSearchResponse>().AsQueryable();
            if (request.Type == "ORD" || request.Type == "DFT")
            {
                var caseDocs = _CaseDocRepo.Entities.Where(w => w.DOId == Guid.Parse(request.Value));
                cases = from c in caseDocs
                        join cd in _CaseRepo.Entites
                        .Include(c=>c.CaseType)
                        .Include(c=>c.CourtBench)
                        .Where(w => w.CreatedBy.Equals(request.UserId))
                         on c.CaseId equals cd.Id
                        select new GetCaseSearchResponse
                        {
                            Id = cd.Id,
                            No=cd.CaseNo,
                            Year=cd.CaseYear.ToString(),
                            CaseType=cd.CaseType.Name_En,
                            Court=cd.CourtBench.CourtBench_En,                            
                            NoYear = cd.CaseNo + "/" + cd.CaseYear,
                            Title = cd.FirstTitle + " V/S " + cd.SecondTitle,
                            Type = cd.CaseType.Name_En,
                            DocFilePath = c.Path
                        };

            }
            if (request.Type == "CST")
            {
                cases = from c in _CaseRepo.Entites
                        .Include(c => c.CaseType)
                        .Include(c => c.CourtBench)
                        .Where(w => w.CaseStageId == Guid.Parse(request.Value))
                        join cd in _CaseDocRepo.Entities
                        .Where(w => w.CreatedBy.Equals(request.UserId)) on c.Id equals cd.CaseId into CaseDocs
                        from doc in CaseDocs.DefaultIfEmpty().Distinct()
                        select new GetCaseSearchResponse
                        {
                            Id = c.Id,
                            No = c.CaseNo,
                            Year = c.CaseYear.ToString(),
                            CaseType = c.CaseType.Name_En,
                            Court = c.CourtBench.CourtBench_En,
                            NoYear = c.CaseNo + "/" + c.CaseYear,
                            Title = c.FirstTitle + " V/S " + c.SecondTitle,
                            Type = c.CaseType.Name_En,
                            DocFilePath = doc.Path
                        };
            }
            if (request.Type == "YER")
            {
                cases = from c in _CaseRepo.Entites.Include(c => c.CaseType).Include(c => c.CourtBench).Where(w => w.CaseYear == Convert.ToInt32(request.Value))
                        join cd in _CaseDocRepo.Entities.Where(w => w.CreatedBy.Equals(request.UserId)) on c.Id equals cd.CaseId into CaseDocs
                        from doc in CaseDocs.DefaultIfEmpty().Distinct()
                        select new GetCaseSearchResponse
                        {
                            Id = c.Id,
                            No = c.CaseNo,
                            Year = c.CaseYear.ToString(),
                            CaseType = c.CaseType.Name_En,
                            Court = c.CourtBench.CourtBench_En,
                            NoYear = c.CaseNo + "/" + c.CaseYear,
                            Title = c.FirstTitle + " V/S " + c.SecondTitle,
                            Type = c.CaseType.Name_En,
                            DocFilePath = doc.Path
                        };
            }
            if (request.Type == "YER")
            {
                cases = from c in _CaseRepo.Entites.Include(c => c.CaseType)
                        .Include(c => c.CourtBench).Where(w => w.CaseYear == Convert.ToInt32(request.Value))
                        join cd in _CaseDocRepo.Entities.Where(w => w.CreatedBy.Equals(request.UserId)) on c.Id equals cd.CaseId into CaseDocs
                        from doc in CaseDocs.DefaultIfEmpty().Distinct()
                        select new GetCaseSearchResponse
                        {
                            Id = c.Id,
                            No = c.CaseNo,
                            Year = c.CaseYear.ToString(),
                            CaseType = c.CaseType.Name_En,
                            Court = c.CourtBench.CourtBench_En,
                            NoYear = c.CaseNo + "/" + c.CaseYear,
                            Title = c.FirstTitle + " V/S " + c.SecondTitle,
                            Type = c.CaseType.Name_En,
                            DocFilePath = doc.Path
                        };
            }
            return await cases.ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
