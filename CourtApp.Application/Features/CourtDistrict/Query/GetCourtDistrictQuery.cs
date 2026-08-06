using CourtApp.Application.Common;
using CourtApp.Application.Features.CourtDistrict.DTOs;
using MediatR;
using System;

namespace CourtApp.Application.Features.CourtDistrict.Query
{
    public class GetCourtDistrictQuery : IRequest<PaginatedResult<CourtDistrictReponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int StateId { get; set; }
    }
}
