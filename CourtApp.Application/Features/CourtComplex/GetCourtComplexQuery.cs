using CourtApp.Application.Common;
using CourtApp.Application.DTOs.CourtComplex;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtComplex
{
    public class GetCourtComplexQuery : IRequest<PaginatedResult<CourtComplexResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int StateId { get; set; }
        public Guid CourtDistrictId { get; set; }
    }
}
