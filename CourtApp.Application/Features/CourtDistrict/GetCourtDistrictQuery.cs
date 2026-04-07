using CourtApp.Application.Common;
using CourtApp.Application.DTOs.CourtDistrict;
using MediatR;
using System;

namespace CourtApp.Application.Features.CourtDistrict
{
    public class GetCourtDistrictQuery : IRequest<PaginatedResult<CourtDistrictReponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int StateId { get; set; }
    }
}
