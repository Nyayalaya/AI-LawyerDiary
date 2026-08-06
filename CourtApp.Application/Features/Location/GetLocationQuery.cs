using CourtApp.Application.Common;
using CourtApp.Application.DTOs.Location;
using MediatR;

namespace CourtApp.Application.Features.Location
{
    public class GetLocationQuery : IRequest<Result<PaginatedResult<LocationResponse>>>
    {
        public int StateId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
