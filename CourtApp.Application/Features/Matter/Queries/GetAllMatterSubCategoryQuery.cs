using CourtApp.Application.Common;
using CourtApp.Application.Features.Matter.Dtos;
using MediatR;
using System;

namespace CourtApp.Application.Features.Matter.Queries
{
    public class GetAllMatterSubCategoryQuery
    : PagedQuery,
      IRequest<PaginatedResult<MatterSubCategoryDto>>
    {
        public Guid? MatterCategoryId { get; set; }
    }
}
