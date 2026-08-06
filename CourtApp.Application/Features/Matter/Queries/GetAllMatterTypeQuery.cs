using CourtApp.Application.Common;
using CourtApp.Application.Features.Matter.Dtos;
using MediatR;

namespace CourtApp.Application.Features.Matter.Queries;

public class GetAllMatterTypeQuery
    : PagedQuery,
      IRequest<PaginatedResult<MatterTypeDto>>
{
}