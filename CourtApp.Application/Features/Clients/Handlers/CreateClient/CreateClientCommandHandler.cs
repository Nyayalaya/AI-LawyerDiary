using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CourtApp.Application.Common;
using CourtApp.Application.Features.Clients.Commands.CreateClient;
using CourtApp.Application.Features.Clients.DTOs;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using CourtApp.Domain.Enums;
using MediatR;

namespace CourtApp.Application.Features.Clients.Handlers.CreateClient
{
    public sealed class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, Result<Guid>>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public CreateClientCommandHandler(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<Result<Guid>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Parse ClientType from string to enum
                if (!Enum.TryParse<ClientType>(request.ClientType, true, out var clientType))
                    return Result<Guid>.Fail($"Invalid client type: {request.ClientType}");

                var clientEntity = new ClientEntity
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    Address = request.Address,
                    Email = request.Email,
                    Mobile = request.Mobile,
                    OfficeEmail = request.OfficeEmail,
                    Phone = request.Phone,
                    ReferalBy = request.ReferralBy,
                    RegNo = request.RegNo,
                    Proprietor = request.Proprietor,
                    ClientType = clientType
                };

                // Create is handled by repository
                var id = await _clientRepository.InsertAsync(clientEntity);
                return Result<Guid>.Success(clientEntity.Id, "Client created successfully");
            }
            catch (Exception ex)
            {
                return Result<Guid>.Fail($"Error creating client: {ex.Message}");
            }
        }
    }
}
