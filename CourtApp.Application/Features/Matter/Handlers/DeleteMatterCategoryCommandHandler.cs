using CourtApp.Application.Common;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.Masters;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Matter.Commands.MatterCategory.Delete;

public sealed class DeleteMatterCategoryCommandHandler
    : IRequestHandler<DeleteMatterCategoryCommand, Result<Guid>>
{
    private readonly IRepositoryAsync<MatterCategoryEntity> _categoryRepository;
    private readonly IRepositoryAsync<MatterSubCategoryEntity> _subCategoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteMatterCategoryCommandHandler(
        IRepositoryAsync<MatterCategoryEntity> categoryRepository,
        IRepositoryAsync<MatterSubCategoryEntity> subCategoryRepository,
        IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _subCategoryRepository = subCategoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(
        DeleteMatterCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await _categoryRepository.GetByIdAsync(request.Id);

        if (entity == null)
            return await Result<Guid>.FailAsync("Matter Category not found.");

        var hasSubCategories = await _subCategoryRepository.Entities
            .AsNoTracking()
            .AnyAsync(x => x.MatterCategoryId == request.Id, cancellationToken);

        if (hasSubCategories)
        {
            return await Result<Guid>.FailAsync(
                "Cannot delete Matter Category because it contains Matter Sub Categories.");
        }

        await _categoryRepository.DeleteAsync(entity);

        await _unitOfWork.Commit(cancellationToken);

        return await Result<Guid>.SuccessAsync(
            entity.Id,
            "Matter Category deleted successfully.");
    }
}