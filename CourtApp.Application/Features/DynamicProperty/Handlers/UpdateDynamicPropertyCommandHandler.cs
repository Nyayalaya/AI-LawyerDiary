using AutoMapper;
using CourtApp.Application.Common;
using CourtApp.Application.Features.DynamicProperty.Commands;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.Masters;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.DynamicProperty.Handlers;

public sealed class UpdateDynamicPropertyCommandHandler
    : IRequestHandler<UpdateDynamicPropertyCommand, Result<System.Guid>>
{
    private readonly IRepositoryAsync<DynamicPropertyEntity> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateDynamicPropertyCommandHandler(
        IRepositoryAsync<DynamicPropertyEntity> repository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<System.Guid>> Handle(UpdateDynamicPropertyCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id);

        if (entity == null)
            return await Result<System.Guid>.FailAsync("Dynamic property not found.");

        request.PropertyCode = request.PropertyCode.Trim().ToUpperInvariant();
        request.PropertyName = request.PropertyName.Trim();

        var exists = await _repository.Entities
            .AsNoTracking()
            .AnyAsync(x => x.Id != request.Id
                           && x.EntityType == request.EntityType
                           && x.EntityId == request.EntityId
                           && x.PropertyCode == request.PropertyCode, cancellationToken);

        if (exists)
            return await Result<System.Guid>.FailAsync("A dynamic property with the same code already exists for the specified entity.");

        _mapper.Map(request, entity);

        await _repository.UpdateAsync(entity);

        await _unitOfWork.Commit(cancellationToken);

        return await Result<System.Guid>.SuccessAsync(entity.Id, "Dynamic property updated successfully.");
    }
}
