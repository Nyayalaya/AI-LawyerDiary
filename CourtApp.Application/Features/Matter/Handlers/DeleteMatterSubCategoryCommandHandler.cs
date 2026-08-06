using CourtApp.Application.Common;
using CourtApp.Application.Features.Matter.Commands;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.Masters;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Matter.Handlers
{
    public class DeleteMatterSubCategoryCommandHandler
    : IRequestHandler<DeleteMatterSubCategoryCommand, Result<Guid>>
    {
        private readonly IRepositoryAsync<MatterSubCategoryEntity> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteMatterSubCategoryCommandHandler(
            IRepositoryAsync<MatterSubCategoryEntity> repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(
            DeleteMatterSubCategoryCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);

            if (entity == null)
                return await Result<Guid>.FailAsync("Matter Sub Category not found.");

            await _repository.DeleteAsync(entity);

            await _unitOfWork.Commit(cancellationToken);

            return await Result<Guid>.SuccessAsync(entity.Id);
        }
    }
}
