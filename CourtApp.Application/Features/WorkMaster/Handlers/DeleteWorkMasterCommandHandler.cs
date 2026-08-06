using CourtApp.Application.Common;
using CourtApp.Application.Features.WorkMaster.Commands;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.WorkMaster.Handlers
{
    public class DeleteWorkMasterCommandHandler : IRequestHandler<DeleteWorkMasterCommand, Result<Guid>>
    {
        private readonly IWorkMasterRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteWorkMasterCommandHandler(IWorkMasterRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(DeleteWorkMasterCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
                return Result<Guid>.Fail("Work Master not found.");

            await _repository.DeleteAsync(entity);
            await _unitOfWork.Commit(cancellationToken);

            return Result<Guid>.Success(entity.Id);
        }
    }
}
