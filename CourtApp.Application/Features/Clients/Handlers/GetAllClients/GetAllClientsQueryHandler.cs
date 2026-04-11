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
    public sealed class GetAllClientsQueryHandler : IRequestHandler<GetAllClientsQuery, Result<List<ClientListDto>>>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public GetAllClientsQueryHandler(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<ClientListDto>>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var clients = await _clientRepository.GetListAsync();
                
                if (!clients.Any())
                    return Result<List<ClientListDto>>.Success(new List<ClientListDto>(), "No clients found");

                var mappedClients = _mapper.Map<List<ClientListDto>>(clients);
                return Result<List<ClientListDto>>.Success(mappedClients);
            }
            catch (System.Exception ex)
            {
                return Result<List<ClientListDto>>.Fail($"Error retrieving clients: {ex.Message}");
            }
        }
    }
}
