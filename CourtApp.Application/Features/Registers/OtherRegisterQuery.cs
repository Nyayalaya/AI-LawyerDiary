using CourtApp.Application.Common;
using CourtApp.Application.DTOs.Registers;
using CourtApp.Application.Features.CaseDetails.Repositories;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Registers
{
    public class OtherRegisterQuery : IRequest<Result<List<OtherRegisterResponse>>>
    {
        public DateTime FromDt { get; set; }
        public DateTime ToDt { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public List<string> LinkedIds { get; set; }
       
    }
    public class OtherRegisterQueryHandler : IRequestHandler<OtherRegisterQuery, Result<List<OtherRegisterResponse>>>
    {
        private readonly IUserCaseRepository _caseRepo;
        private readonly ICaseProceedingRepository _ProcRepo;
        private readonly IWorkMasterRepository _wRepo;
        private readonly IUserCaseRepository _repository;
        private readonly ICaseAssignedRepository _assignRepo;
        public OtherRegisterQueryHandler(IUserCaseRepository _caseRepo,
            IWorkMasterRepository _wRepo,
            ICaseProceedingRepository procRepo,
            IUserCaseRepository _repository,
            ICaseAssignedRepository _assignRepo
            )
        {
            this._caseRepo = _caseRepo;
            this._wRepo = _wRepo;
            _ProcRepo = procRepo;
            this._repository = _repository;
            this._assignRepo = _assignRepo;
        }
        public async Task<Result<List<OtherRegisterResponse>>> Handle(OtherRegisterQuery request, CancellationToken cancellationToken)
        {
            var caseResponses = await (
                        from p in _ProcRepo.Entities
                            .Where(w => request.LinkedIds.Contains(w.CreatedBy))
                            .Include(p => p.Case)
                                .ThenInclude(c => c.CaseType)
                            .Include(p => p.Case)
                                .ThenInclude(c => c.CourtBench)
                            .Include(p => p.ProcWork)
                                .ThenInclude(pw => pw.Works)
                        join ac in _assignRepo.Entities on p.Case.Id equals ac.CaseId
                                                into caseAssignments
                        from ac in caseAssignments.DefaultIfEmpty()
                        from work in p.ProcWork.Works
                        join w in _wRepo.Entities.Where(w => !w.Code.Equals("COPY"))
                            on work.WorkTypeId equals w.Id
                        let c = p.Case
                        where c != null && (request.LinkedIds.Contains(c.CreatedBy) ||
                                (ac != null && request.LinkedIds.Contains(ac.LawyerId.ToString())))
                        select new OtherRegisterResponse
                        {
                            Id = c.Id,
                            Reference = ac != null && request.LinkedIds.Contains(ac.LawyerId.ToString()) ? "Assigned" : "Self",
                            CaseType = c.CaseType != null ? c.CaseType.Name_En : string.Empty,
                            CaseYear = c.CaseYear,
                            Court = c.CourtBench != null ? c.CourtBench.CourtBench_En : string.Empty,
                            CaseTitle = c.FirstTitle + " VS " + c.SecondTitle,
                            CaseNumber = c.CaseNo,
                            Status = c.DisposalDate.HasValue ? "Disposed" : "Pending",
                            WorkDone = w.Name,
                            WorkDate = w.LastModifiedOn.HasValue
                                ? w.LastModifiedOn.Value.ToString("dd/MM/yyyy")
                                : "-"
                        }
                        ).ToListAsync();
            if (caseResponses.Count > 0)
                return Result<List<OtherRegisterResponse>>.Success(caseResponses);
            return Result<List<OtherRegisterResponse>>.Fail("No record found");
        }
    }
}
