using CourtApp.Application.Common;
using CourtApp.Application.Features.CourtDistrict.DTOs;
using MediatR;
using System;
using System.Collections.Generic;

namespace CourtApp.Application.Features.CourtDistrict.Query
{
    public class GetCourtDistrictDropdownQuery : IRequest<PaginatedResult<CourtDistrictDropdownResponse>>
    {
        public int? Skip { get; set; } = 0;
        public int? PageSize { get; set; }
        public string SearchTerm { get; set; }
    }
}
