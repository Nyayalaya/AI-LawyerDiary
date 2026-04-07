using CourtApp.Application.Common;
using CourtApp.Application.Features.WorkMasterSub.Commands;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.WorkMasterSub.Handlers
{
    public class DeleteWorkSubMasterCommandHandler : IRequestHandler<DeleteWorkSubMasterCommand, Result<Guid>>
    {
        private readonly IWorkMasterSubRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteWorkSubMasterCommandHandler(IWorkMasterSubRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(DeleteWorkSubMasterCommand request, CancellationToken cancellationToken)
        {
            var detail = await _repository.GetByIdAsync(request.Id);
            if (detail == null)
                return Result<Guid>.Fail("Work Sub Master record not found.");

            await _repository.DeleteAsync(detail);
            await _unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success(detail.Id);
        }
    }
}
