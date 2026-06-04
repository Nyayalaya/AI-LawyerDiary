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
    public class GetPermissionSlipQuery : IRequest<Result<List<PermissionSlipResponse>>>
    {
        public List<Guid> CaseIds { get; set; }
    }
    public class GetPermissionSlipQueryHandler : IRequestHandler<GetPermissionSlipQuery, Result<List<PermissionSlipResponse>>>
    {
        private readonly IUserCaseRepository _CaseRepo;
        private readonly ICaseProceedingRepository _wRepo;
        //private readonly ICaseAgainstRepository _wAgainstRepo;
        public GetPermissionSlipQueryHandler(IUserCaseRepository _CaseRepo, ICaseProceedingRepository _wRepo/*, ICaseAgainstRepository wAgainstRepo*/)
        {
            this._CaseRepo = _CaseRepo;
            this._wRepo = _wRepo;
            //_wAgainstRepo = wAgainstRepo;
        }
        public async Task<Result<List<PermissionSlipResponse>>> Handle(GetPermissionSlipQuery request, CancellationToken cancellationToken)
        {

            var Cases = _CaseRepo.Entites
                        .Include(a => a.CaseAgainstEntities)
                        .Include(p => p.CaseProcEntities)
                        .Where(w => request.CaseIds.Contains(w.Id))
                        .Select(cd => new PermissionSlipResponse
                        {
                            NoYear = cd.CaseNo + "/" + cd.CaseYear,
                            CaseType = cd.CaseType.Name_En,
                            Title = cd.FirstTitle + " Vs " + cd.SecondTitle,
                            DoP = cd.InstitutionDate.ToString("dd/MM/yyyy"),
                            MatterGo = cd.CaseStage != null ? cd.CaseStage.Name : "",
                            //DoI = cd.CaseAgainstEntities != null && cd.CaseAgainstEntities.Any() ? cd.CaseAgainstEntities.FirstOrDefault().ImpugedOrderDate?.ToString("dd/MM/yyyy") ?? "" : "",
                            NextDate = cd.NextDate.HasValue && cd.CaseProcEntities.Any()
                                        ? cd.CaseProcEntities.Max(p => p.NextDate.HasValue ? p.NextDate.Value : DateTime.MinValue) > cd.NextDate.Value
                                        ? cd.CaseProcEntities.Max(p => p.NextDate.HasValue ? p.NextDate.Value : DateTime.MinValue).ToString("dd/MM/yyyy")
                                        : cd.NextDate.Value.ToString("dd/MM/yyyy")
                                        : cd.NextDate.HasValue
                                        ? cd.NextDate.Value.ToString("dd/MM/yyyy")
                                        : cd.CaseProcEntities.Max(p => p.NextDate.HasValue ? p.NextDate.Value : DateTime.MinValue).ToString("dd/MM/yyyy")
                        }).ToList();

            return await Result<List<PermissionSlipResponse>>.SuccessAsync(Cases.ToList());
        }
    }
}
