using System.Collections.Generic;
using CourtApp.Application.Common;
using CourtApp.Application.Features.Clients.DTOs;
using MediatR;

namespace CourtApp.Application.Features.Clients.Queries.SearchClients
{
    public sealed class SearchClientsQuery : IRequest<Result<PaginatedResult<ClientListDto>>>
    {
        public string SearchTerm { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
