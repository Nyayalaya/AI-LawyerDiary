using CourtApp.Application.Common;
using MediatR;

namespace CourtApp.Application.Features.CourtLevel.Query
{
    public class GetCourtLevelQuery : IRequest<PaginatedResult<GetCourtLevelResponse>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
