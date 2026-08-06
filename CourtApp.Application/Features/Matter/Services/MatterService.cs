using CourtApp.Application.Common;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.Masters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Matter.Services;

public sealed class MatterService : IMatterService
{
    private readonly IRepositoryAsync<MatterTypeEntity> _matterTypeRepository;
    private readonly IRepositoryAsync<MatterCategoryEntity> _matterCategoryRepository;
    private readonly IRepositoryAsync<MatterSubCategoryEntity> _matterSubCategoryRepository;

    public MatterService(
        IRepositoryAsync<MatterTypeEntity> matterTypeRepository,
        IRepositoryAsync<MatterCategoryEntity> matterCategoryRepository,
        IRepositoryAsync<MatterSubCategoryEntity> matterSubCategoryRepository)
    {
        _matterTypeRepository = matterTypeRepository;
        _matterCategoryRepository = matterCategoryRepository;
        _matterSubCategoryRepository = matterSubCategoryRepository;
    }

    #region Matter Type

    public async Task<Result> ValidateMatterTypeAsync(
        Guid? id,
        string code,
        string name,
        CancellationToken cancellationToken)
    {
        code = code.Trim().ToUpperInvariant();
        name = name.Trim();

        var duplicate = await _matterTypeRepository.Entities
            .AsNoTracking()
            .AnyAsync(x =>
                x.Id != id &&
                (x.Code == code || x.Name == name),
                cancellationToken);

        if (duplicate)
        {
            return await Result.FailAsync(
                "Matter Type with the same Code or Name already exists.");
        }

        return await Result.SuccessAsync();
    }

    #endregion

    #region Matter Category

    public async Task<Result> ValidateMatterCategoryAsync(
        Guid? id,
        Guid matterTypeId,
        string code,
        string name,
        CancellationToken cancellationToken)
    {
        var parentExists = await _matterTypeRepository.Entities
            .AsNoTracking()
            .AnyAsync(x => x.Id == matterTypeId, cancellationToken);

        if (!parentExists)
        {
            return await Result.FailAsync("Matter Type not found.");
        }

        code = code.Trim().ToUpperInvariant();
        name = name.Trim();

        var duplicate = await _matterCategoryRepository.Entities
            .AsNoTracking()
            .AnyAsync(x =>
                x.Id != id &&
                x.MatterTypeId == matterTypeId &&
                (x.Code == code || x.Name == name),
                cancellationToken);

        if (duplicate)
        {
            return await Result.FailAsync(
                "Matter Category with the same Code or Name already exists.");
        }

        return await Result.SuccessAsync();
    }

    #endregion

    #region Matter Sub Category

    public async Task<Result> ValidateMatterSubCategoryAsync(
        Guid? id,
        Guid matterCategoryId,
        string code,
        string name,
        CancellationToken cancellationToken)
    {
        var parentExists = await _matterCategoryRepository.Entities
            .AsNoTracking()
            .AnyAsync(x => x.Id == matterCategoryId, cancellationToken);

        if (!parentExists)
        {
            return await Result.FailAsync("Matter Category not found.");
        }

        code = code.Trim().ToUpperInvariant();
        name = name.Trim();

        var duplicate = await _matterSubCategoryRepository.Entities
            .AsNoTracking()
            .AnyAsync(x =>
                x.Id != id &&
                x.MatterCategoryId == matterCategoryId &&
                (x.Code == code || x.Name == name),
                cancellationToken);

        if (duplicate)
        {
            return await Result.FailAsync(
                "Matter Sub Category with the same Code or Name already exists.");
        }

        return await Result.SuccessAsync();
    }

    #endregion
}