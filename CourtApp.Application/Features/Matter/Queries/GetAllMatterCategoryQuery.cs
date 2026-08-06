using CourtApp.Application.Common;
using CourtApp.Application.Features.Matter.Dtos;
using MediatR;
using System;
using System.Collections.Generic;

namespace CourtApp.Application.Features.Matter.Queries;

public class GetAllMatterCategoryQuery
    : PagedQuery,
      IRequest<PaginatedResult<MatterCategoryDto>>
{
    public Guid? MatterTypeId { get; set; }
}