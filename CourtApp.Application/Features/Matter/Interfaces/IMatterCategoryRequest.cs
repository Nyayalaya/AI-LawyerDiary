using System;

namespace CourtApp.Application.Features.Matter.Interfaces;

public interface IMatterCategoryRequest
{
    Guid MatterTypeId { get; }

    string Code { get; }

    string Name { get; }

    string? Description { get; }

    int DisplayOrder { get; }
}