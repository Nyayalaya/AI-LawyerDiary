using CourtApp.Application.Common;
using CourtApp.Application.DTOs.DropdownDtos;
using MediatR;
using System;
using System.Collections.Generic;

namespace CourtApp.Application.Features.Matter.Queries;

public sealed record GetMatterCategoryDropdownQuery
    : IRequest<Result<List<DdlGuidStringDto>>>
{
    public Guid MatterTypeId { get; init; }
}
