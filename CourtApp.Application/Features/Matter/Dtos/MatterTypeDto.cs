using System;

namespace CourtApp.Application.Features.Matter.Dtos;

public class MatterTypeDto
{
    public Guid Id { get; set; }

    public string Code { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int DisplayOrder { get; set; }
}