using CourtApp.Application.Common;
using CourtApp.Application.Features.CaseDetails.Commands;
using CourtApp.Application.Features.CaseDetails.Repositories;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseDetails.Handlers
{
    public class CaseDeAssignedCommnadHandler : IRequestHandler<CaseDeAssignedCommnad, Result<string>>
    {
        private readonly ICaseAssignedRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CaseDeAssignedCommnadHandler(ICaseAssignedRepository repository, IUnitOfWork unitOfWork)
        {
            this._repository = repository; ;
            this._unitOfWork = unitOfWork;
        }
        public async Task<Result<string>> Handle(CaseDeAssignedCommnad request, CancellationToken cancellationToken)
        {
            var assignedCaseDetails = _repository
                .Entities
                .Where(w => w.CaseId == request.CaseId
                && w.LawyerId == request.LawyerId).ToList();
            if (assignedCaseDetails == null)
                return await Result<string>.FailAsync("There is no case avaiable for de-assigning");

            var deAssigned = _repository.DeleteRangeAsync(assignedCaseDetails);
            await _unitOfWork.Commit(cancellationToken);
            return await Result<string>.SuccessAsync();
        }
    }
}
