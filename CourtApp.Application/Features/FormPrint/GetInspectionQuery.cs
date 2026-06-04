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
using CourtApp.Application.Features.FormPrint.Dtos;


namespace CourtApp.Application.Features.FormPrint
{
    public class GetInspectionQuery : IRequest<Result<List<InspectionResponse>>>
    {
        public List<Guid> CaseIds { get; set; }
    }
    public class GetCasesByIdsQueryHandler : IRequestHandler<GetInspectionQuery, Result<List<InspectionResponse>>>
    {
        private readonly IUserCaseRepository _CaseRepo;
        private readonly ICaseProceedingRepository _wRepo;
        private readonly IMapper _mapper;
        private readonly IClientRepository _ClientRepo;
        private readonly IFSTitleRepository _AppeareceRepo;
        public GetCasesByIdsQueryHandler(IUserCaseRepository _CaseRepo, IMapper _mapper,
            ICaseProceedingRepository wRepo, IClientRepository clientRepo, IFSTitleRepository _AppeareceRepo)
        {
            this._CaseRepo = _CaseRepo;
            this._mapper = _mapper;
            _wRepo = wRepo;
            _ClientRepo = clientRepo;
            this._AppeareceRepo = _AppeareceRepo;
        }
        public async Task<Result<List<InspectionResponse>>> Handle(GetInspectionQuery request, CancellationToken cancellationToken)
        {

            var Cases = await _CaseRepo.Entites
                       .Include(a => a.CaseAgainstEntities)
                       .Include(p => p.CaseProcEntities)
                       .Include(c => c.CourtType)
                       .Include(c => c.CourtBench)
                       .Include(c => c.FTitle)
                       .Where(w => request.CaseIds.Contains(w.Id))
                       .Select(cd => new InspectionResponse
                       {
                           NoYear = cd.CaseNo + "/" + cd.CaseYear,
                           CaseType = cd.CaseType.Name_En,
                           Title = cd.FirstTitle + " Vs " + cd.SecondTitle,
                           CourtName = cd.CourtBench.CourtBench_En,
                           Appearence = cd.FTitle.Name_En,
                           NextDate = cd.NextDate.HasValue && cd.CaseProcEntities.Any()
                                       ? cd.CaseProcEntities.Max(p => p.NextDate.HasValue ? p.NextDate.Value : DateTime.MinValue) > cd.NextDate.Value
                                       ? cd.CaseProcEntities.Max(p => p.NextDate.HasValue ? p.NextDate.Value : DateTime.MinValue).ToString("dd/MM/yyyy")
                                       : cd.NextDate.Value.ToString("dd/MM/yyyy")
                                       : cd.NextDate.HasValue
                                       ? cd.NextDate.Value.ToString("dd/MM/yyyy")
                                       : cd.CaseProcEntities.Max(p => p.NextDate.HasValue ? p.NextDate.Value : DateTime.MinValue).ToString("dd/MM/yyyy")
                       }).ToListAsync();
            return Result<List<InspectionResponse>>.Success(Cases.ToList());
        }
    }
}
