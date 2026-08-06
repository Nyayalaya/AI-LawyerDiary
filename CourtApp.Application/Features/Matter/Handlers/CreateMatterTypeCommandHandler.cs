using AutoMapper;
using CourtApp.Application.Common;
using CourtApp.Application.Features.Matter.Commands;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.Masters;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Matter.Handlers;

public sealed class CreateMatterTypeCommandHandler
    : IRequestHandler<CreateMatterTypeCommand, Result<Guid>>
{
    private readonly IRepositoryAsync<MatterTypeEntity> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateMatterTypeCommandHandler(
        IRepositoryAsync<MatterTypeEntity> repository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<Guid>> Handle(
        CreateMatterTypeCommand request,
        CancellationToken cancellationToken)
    {
        request.Code = request.Code.Trim().ToUpperInvariant();
        request.Name = request.Name.Trim();
        request.Description = request.Description?.Trim();

        var exists = await _repository.Entities
            .AsNoTracking()
            .AnyAsync(x =>
                    x.Code == request.Code ||
                    x.Name == request.Name,
                cancellationToken);

        if (exists)
        {
            return await Result<Guid>.FailAsync(
                "Matter Type with the same Code or Name already exists.");
        }

        var entity = _mapper.Map<MatterTypeEntity>(request);

        await _repository.AddAsync(entity);

        await _unitOfWork.Commit(cancellationToken);

        return await Result<Guid>.SuccessAsync(
            entity.Id,
            "Matter Type created successfully.");
    }
}