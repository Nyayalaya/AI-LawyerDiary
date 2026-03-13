
using CourtApp.Application.Common;
using MediatR;

namespace CourtApp.Application.Features.CourtType.Query
{
    public class SearchCourtTypesQuery : IRequest<Result<PaginatedResult<GetCourtTypeResponse>>>
    {
        public string Keyword { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}