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

public sealed class CreateDynamicPropertyCommandHandler
    : IRequestHandler<CreateDynamicPropertyCommand, Result<System.Guid>>
{
    private readonly IRepositoryAsync<DynamicPropertyEntity> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateDynamicPropertyCommandHandler(
        IRepositoryAsync<DynamicPropertyEntity> repository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<System.Guid>> Handle(CreateDynamicPropertyCommand request, CancellationToken cancellationToken)
    {
        request.PropertyCode = request.PropertyCode.Trim().ToUpperInvariant();
        request.PropertyName = request.PropertyName.Trim();

        var exists = await _repository.Entities
            .AsNoTracking()
            .AnyAsync(x => x.EntityType == request.EntityType
                           && x.EntityId == request.EntityId
                           && x.PropertyCode == request.PropertyCode, cancellationToken);

        if (exists)
            return await Result<System.Guid>.FailAsync("A dynamic property with the same code already exists for the specified entity.");

        var entity = _mapper.Map<DynamicPropertyEntity>(request);

        await _repository.AddAsync(entity);

        await _unitOfWork.Commit(cancellationToken);

        return await Result<System.Guid>.SuccessAsync(entity.Id, "Dynamic property created successfully.");
    }
}
