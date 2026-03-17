using CourtApp.Application.Common;
using MediatR;
using System.Collections.Generic;

namespace CourtApp.Application.Features.Clients.Queries.GetAllCached
{
    public class GetAllClientCachedQuery : IRequest<PaginatedResult<GetAllClientCachedResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public List<string> LinkedIds { get; set; }
    }
}