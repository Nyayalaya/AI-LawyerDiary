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

public sealed class DeleteMatterTypeCommandHandler
    : IRequestHandler<DeleteMatterTypeCommand, Result<Guid>>
{
    private readonly IRepositoryAsync<MatterTypeEntity> _matterTypeRepository;
    private readonly IRepositoryAsync<MatterCategoryEntity> _matterCategoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteMatterTypeCommandHandler(
        IRepositoryAsync<MatterTypeEntity> matterTypeRepository,
        IRepositoryAsync<MatterCategoryEntity> matterCategoryRepository,
        IUnitOfWork unitOfWork)
    {
        _matterTypeRepository = matterTypeRepository;
        _matterCategoryRepository = matterCategoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(
        DeleteMatterTypeCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await _matterTypeRepository.GetByIdAsync(request.Id);

        if (entity == null)
        {
            return await Result<Guid>.FailAsync("Matter Type not found.");
        }

        var hasCategories = await _matterCategoryRepository.Entities
            .AsNoTracking()
            .AnyAsync(x => x.MatterTypeId == request.Id, cancellationToken);

        if (hasCategories)
        {
            return await Result<Guid>.FailAsync(
                "This Matter Type cannot be deleted because one or more Matter Categories are associated with it.");
        }

        await _matterTypeRepository.DeleteAsync(entity);

        await _unitOfWork.Commit(cancellationToken);

        return await Result<Guid>.SuccessAsync(
            entity.Id,
            "Matter Type deleted successfully.");
    }
}