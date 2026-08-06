using CourtApp.Application.Common;
using CourtApp.Application.Features.DynamicProperty.Dtos;
using MediatR;
using System;

namespace CourtApp.Application.Features.DynamicProperty.Queries;

public class GetAllDynamicPropertyQuery
    : PagedQuery,
      IRequest<Result<PaginatedResult<DynamicPropertyDto>>>
{
    public string? EntityType { get; set; }

    public Guid? EntityId { get; set; }

    public bool? IsActive { get; set; }
}
