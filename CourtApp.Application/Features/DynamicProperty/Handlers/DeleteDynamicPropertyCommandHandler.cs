using CourtApp.Application.Common;
using CourtApp.Application.Features.DynamicProperty.Commands;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.Masters;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.DynamicProperty.Handlers;

public sealed class DeleteDynamicPropertyCommandHandler
    : IRequestHandler<DeleteDynamicPropertyCommand, Result<System.Guid>>
{
    private readonly IRepositoryAsync<DynamicPropertyEntity> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteDynamicPropertyCommandHandler(
        IRepositoryAsync<DynamicPropertyEntity> repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<System.Guid>> Handle(DeleteDynamicPropertyCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id);

        if (entity == null)
            return await Result<System.Guid>.FailAsync("Dynamic property not found.");

        await _repository.DeleteAsync(entity);

        await _unitOfWork.Commit(cancellationToken);

        return await Result<System.Guid>.SuccessAsync(entity.Id, "Dynamic property deleted successfully.");
    }
}
