using CourtApp.Application.Common;
using CourtApp.Application.Features.FormPrint.Dtos;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.FormPrint
{
    public class GetCopyingAppQuery : IRequest<Result<List<CopyingAppResponse>>>
    {
        public List<Guid> CaseIds { get; set; }
    }
    public class GetCopyingAppQueryHandler : IRequestHandler<GetCopyingAppQuery, Result<List<CopyingAppResponse>>>
    {
        private readonly IUserCaseRepository _CaseRepo;
        private readonly IFSTitleRepository _AppeareceRepo;
        private readonly IClientRepository _ClientRepo;
        private readonly ICaseProceedingRepository _wRepo;
        private readonly ILawyerRepository _LawyerRepo;

        public GetCopyingAppQueryHandler(IUserCaseRepository _CaseRepo,
            IFSTitleRepository _cachedRepo,
            IClientRepository clientRepo,
            ICaseProceedingRepository _wRepo,
            ILawyerRepository _LawyerRepo
            )
        {
            this._CaseRepo = _CaseRepo;
            this._AppeareceRepo = _cachedRepo;
            _ClientRepo = clientRepo;
            this._wRepo = _wRepo;
            this._LawyerRepo = _LawyerRepo;
        }
        public async Task<Result<List<CopyingAppResponse>>> Handle(GetCopyingAppQuery request, CancellationToken cancellationToken)
        {
            var Cases = await _CaseRepo.Entites
                       .Include(a => a.CaseAgainstEntities)
                       .Include(p => p.CaseProcEntities)
                       .Include(c => c.CourtType)
                       .Include(c => c.CourtBench)
                       .Include(c => c.FTitle)
                       .Include(c => c.STitle)
                       .Include(c => c.Client)
                       .Where(w => request.CaseIds.Contains(w.Id))
                       .Select(cd => new CopyingAppResponse
                       {
                           FirstTitle = cd.FirstTitle,
                           SecondTitle = cd.SecondTitle,
                           NoYear = cd.CaseNo + "/" + cd.CaseYear,
                           CaseType = cd.CaseType.Name_En,
                           CourtType = cd.CourtType.Code,
                           Court = cd.CourtBench.CourtBench_En,
                           Appearence = "",
                           NextDate = cd.NextDate.HasValue && cd.CaseProcEntities.Any()
                                       ? cd.CaseProcEntities.Max(p => p.NextDate.HasValue ? p.NextDate.Value : DateTime.MinValue) > cd.NextDate.Value
                                       ? cd.CaseProcEntities.Max(p => p.NextDate.HasValue ? p.NextDate.Value : DateTime.MinValue).ToString("dd/MM/yyyy")
                                       : cd.NextDate.Value.ToString("dd/MM/yyyy")
                                       : cd.NextDate.HasValue
                                       ? cd.NextDate.Value.ToString("dd/MM/yyyy")
                                       : cd.CaseProcEntities.Max(p => p.NextDate.HasValue ? p.NextDate.Value : DateTime.MinValue).ToString("dd/MM/yyyy")
                       }).ToListAsync();

            return Result<List<CopyingAppResponse>>.Success(Cases.ToList());
        }
    }
}
