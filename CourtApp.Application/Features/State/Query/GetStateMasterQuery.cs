using CourtApp.Application.Common;
using MediatR;

namespace CourtApp.Application.Features.State.Query
{
    public class GetStateMasterQuery : IRequest<PaginatedResult<GetStateMasterResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; } 
    }
}