using CourtApp.Application.Common;
using CourtApp.Domain.Enums;
using MediatR;
using System;

namespace CourtApp.Application.Features.DynamicProperty.Commands;

public sealed class CreateDynamicPropertyCommand : IRequest<Result<Guid>>
{
    public string EntityType { get; set; } = string.Empty;

    public Guid EntityId { get; set; }

    public string PropertyCode { get; set; } = string.Empty;

    public string PropertyName { get; set; } = string.Empty;

    public string? Description { get; set; }

    public DynamicDataType DataType { get; set; }

    public DynamicControlType ControlType { get; set; }

    public bool IsRequired { get; set; }

    public bool IsVisible { get; set; } = true;

    public bool IsSearchable { get; set; }

    public bool IsReadOnly { get; set; }

    public bool AllowMultiple { get; set; }

    public int DisplayOrder { get; set; }

    public int? MaxLength { get; set; }

    public decimal? MinValue { get; set; }

    public decimal? MaxValue { get; set; }

    public string? DefaultValue { get; set; }

    public string? ValidationRegex { get; set; }

    public string? LookupSource { get; set; }

    public string? Placeholder { get; set; }

    public string? HelpText { get; set; }

    public bool IsActive { get; set; } = true;
}
