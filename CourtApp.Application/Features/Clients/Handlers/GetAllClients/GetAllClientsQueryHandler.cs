using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CourtApp.Application.Common;
using CourtApp.Application.Features.Clients.DTOs;
using CourtApp.Application.Features.Clients.Queries.GetAllClients;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;

namespace CourtApp.Application.Features.Clients.Handlers.GetAllClients
{
    public sealed class GetAllClientsQueryHandler : IRequestHandler<GetAllClientsQuery, PaginatedResult<ClientListDto>>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public GetAllClientsQueryHandler(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<ClientListDto>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var clients = await _clientRepository.GetListAsync();

                if (!clients.Any())
                    return PaginatedResult<ClientListDto>.Success(
                        new List<ClientListDto>(),
                        0,
                        request.PageNumber,
                        request.PageSize,
                        "No clients found");

                var mappedClients = _mapper.Map<List<ClientListDto>>(clients);
                return PaginatedResult<ClientListDto>.Success(
                    mappedClients,
                    mappedClients.Count,
                    request.PageNumber,
                    request.PageSize,
                    "Clients retrieved successfully");
            }
            catch (System.Exception ex)
            {
                return PaginatedResult<ClientListDto>.Failure($"Error retrieving clients: {ex.Message}");
            }
        }
    }
}
