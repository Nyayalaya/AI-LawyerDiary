using System;

namespace CourtApp.Application.Features.Matter.Interfaces;

public interface IMatterSubCategoryRequest
{
    Guid MatterCategoryId { get; }

    string Code { get; }

    string Name { get; }

    string? Description { get; }

    int DisplayOrder { get; }
}