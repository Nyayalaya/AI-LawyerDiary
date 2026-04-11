using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CourtApp.Application.Common;
using CourtApp.Application.Features.Clients.DTOs;
using CourtApp.Application.Features.Clients.Queries.GetClientById;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;

namespace CourtApp.Application.Features.Clients.Handlers.GetClientById
{
    public sealed class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery, Result<ClientResponseDto>>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public GetClientByIdQueryHandler(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<Result<ClientResponseDto>> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var client = await _clientRepository.GetByIdAsync(request.Id);

                if (client == null)
                    return Result<ClientResponseDto>.Fail("Client not found");

                var mappedClient = _mapper.Map<ClientResponseDto>(client);
                return Result<ClientResponseDto>.Success(mappedClient);
            }
            catch (System.Exception ex)
            {
                return Result<ClientResponseDto>.Fail($"Error retrieving client: {ex.Message}");
            }
        }
    }
}
