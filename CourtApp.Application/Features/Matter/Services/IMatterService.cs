using CourtApp.Application.Common;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace CourtApp.Application.Features.Matter.Services;

public interface IMatterService
{
    Task<Result> ValidateMatterTypeAsync(
        Guid? id,
        string code,
        string name,
        CancellationToken cancellationToken);

    Task<Result> ValidateMatterCategoryAsync(
        Guid? id,
        Guid matterTypeId,
        string code,
        string name,
        CancellationToken cancellationToken);

    Task<Result> ValidateMatterSubCategoryAsync(
        Guid? id,
        Guid matterCategoryId,
        string code,
        string name,
        CancellationToken cancellationToken);
}