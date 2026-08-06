using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CourtApp.Application.Common;
using CourtApp.Application.Features.Clients.DTOs;
using CourtApp.Application.Features.Clients.Queries.SearchClients;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;

namespace CourtApp.Application.Features.Clients.Handlers.SearchClients
{
    public sealed class SearchClientsQueryHandler : IRequestHandler<SearchClientsQuery, Result<PaginatedResult<ClientListDto>>>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public SearchClientsQueryHandler(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<Result<PaginatedResult<ClientListDto>>> Handle(SearchClientsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var clients = await _clientRepository.GetListAsync();
                
                // Filter by search term
                if (!string.IsNullOrWhiteSpace(request.SearchTerm))
                {
                    clients = clients
                        .Where(c => c.Name.Contains(request.SearchTerm, System.StringComparison.OrdinalIgnoreCase) ||
                                   c.Email.Contains(request.SearchTerm, System.StringComparison.OrdinalIgnoreCase) ||
                                   c.Mobile.Contains(request.SearchTerm, System.StringComparison.OrdinalIgnoreCase))
                        .ToList();
                }

                var totalCount = clients.Count();
                
                // Pagination
                var paginatedClients = clients
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToList();

                var mappedClients = _mapper.Map<List<ClientListDto>>(paginatedClients);

                var paginatedResult = PaginatedResult<ClientListDto>.Success(
                    mappedClients, 
                    totalCount, 
                    request.PageNumber, 
                    request.PageSize
                );

                return Result<PaginatedResult<ClientListDto>>.Success(paginatedResult);
            }
            catch (System.Exception ex)
            {
                return Result<PaginatedResult<ClientListDto>>.Fail($"Error searching clients: {ex.Message}");
            }
        }
    }
}
