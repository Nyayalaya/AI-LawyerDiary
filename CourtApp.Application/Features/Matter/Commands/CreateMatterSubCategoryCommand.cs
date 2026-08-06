using CourtApp.Application.Common;
using CourtApp.Application.Features.Matter.Interfaces;
using MediatR;
using System;
namespace CourtApp.Application.Features.Matter.Commands;

public class CreateMatterSubCategoryCommand
    : IRequest<Result<Guid>>, IMatterSubCategoryRequest
{
    public Guid MatterCategoryId { get; set; }

    public string Code { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int DisplayOrder { get; set; }
}