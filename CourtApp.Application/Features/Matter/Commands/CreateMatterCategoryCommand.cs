using CourtApp.Application.Common;
using CourtApp.Application.Features.Matter.Interfaces;
using MediatR;
using System;

namespace CourtApp.Application.Features.Matter.Commands.MatterCategory.Create;

public sealed class CreateMatterCategoryCommand
    : IRequest<Result<Guid>>, IMatterCategoryRequest
{
    public Guid MatterTypeId { get; set; }

    public string Code { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int DisplayOrder { get; set; }
}