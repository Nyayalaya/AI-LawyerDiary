using System.Collections.Generic;
using CourtApp.Application.Common;
using CourtApp.Application.Features.Clients.DTOs;
using MediatR;

namespace CourtApp.Application.Features.Clients.Queries.GetAllClients
{
    public sealed class GetAllClientsQuery : IRequest<PaginatedResult<ClientListDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SearchTerm { get; set; }
    }
}
