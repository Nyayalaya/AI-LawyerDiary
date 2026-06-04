using AutoMapper;
using CourtApp.Application.Common;
using CourtApp.Application.Features.CaseDetails.Commands;
using CourtApp.Application.Features.CaseDetails.Repositories;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.CaseDetails;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseDetails.Handlers
{
    public class CaseAssignedCommandHandler : IRequestHandler<CaseAssignedCommand, Result<string>>
    {
        private readonly ICaseAssignedRepository _caseAssigned;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public CaseAssignedCommandHandler(ICaseAssignedRepository caseAssigned, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _caseAssigned = caseAssigned;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<string>> Handle(CaseAssignedCommand request, CancellationToken cancellationToken)
        {
            if (request != null)
            {
                var entity = _mapper.Map<CaseAssignedEntity>(request);
                await _caseAssigned.InsertAsync(entity);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<string>.SuccessAsync("Case assigned successfully!");
            }
            return await Result<string>.FailAsync("Invalid case assigned information");
        }
    }
}
