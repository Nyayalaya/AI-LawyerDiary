using AutoMapper;
using CourtApp.Application.Common;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.Masters;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Matter.Commands.MatterCategory.Update;

public sealed class UpdateMatterCategoryCommandHandler
    : IRequestHandler<UpdateMatterCategoryCommand, Result<Guid>>
{
    private readonly IRepositoryAsync<MatterCategoryEntity> _categoryRepository;
    private readonly IRepositoryAsync<MatterTypeEntity> _matterTypeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateMatterCategoryCommandHandler(
        IRepositoryAsync<MatterCategoryEntity> categoryRepository,
        IRepositoryAsync<MatterTypeEntity> matterTypeRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _matterTypeRepository = matterTypeRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<Guid>> Handle(
        UpdateMatterCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await _categoryRepository.GetByIdAsync(request.Id);

        if (entity == null)
            return await Result<Guid>.FailAsync("Matter Category not found.");

        var matterTypeExists = await _matterTypeRepository.Entities
            .AsNoTracking()
            .AnyAsync(x => x.Id == request.MatterTypeId, cancellationToken);

        if (!matterTypeExists)
            return await Result<Guid>.FailAsync("Matter Type not found.");

        request.Code = request.Code.Trim().ToUpperInvariant();
        request.Name = request.Name.Trim();
        request.Description = request.Description?.Trim();

        var duplicate = await _categoryRepository.Entities
            .AsNoTracking()
            .AnyAsync(x =>
                x.Id != request.Id &&
                x.MatterTypeId == request.MatterTypeId &&
                (x.Code == request.Code || x.Name == request.Name),
                cancellationToken);

        if (duplicate)
            return await Result<Guid>.FailAsync(
                "Matter Category with the same Code or Name already exists.");

        _mapper.Map(request, entity);

        await _categoryRepository.UpdateAsync(entity);

        await _unitOfWork.Commit(cancellationToken);

        return await Result<Guid>.SuccessAsync(
            entity.Id,
            "Matter Category updated successfully.");
    }
}