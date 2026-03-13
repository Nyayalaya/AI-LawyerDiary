
using CourtApp.Application.Common;
using MediatR;

namespace CourtApp.Application.Features.CourtType.Query
{
    public class GetAllCourtTypesQuery : IRequest<Result<PaginatedResult<GetCourtTypeResponse>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}