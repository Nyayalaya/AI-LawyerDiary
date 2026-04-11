using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CourtApp.Application.Common;
using CourtApp.Application.Features.Clients.Commands.UpdateClient;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Enums;
using MediatR;

namespace CourtApp.Application.Features.Clients.Handlers.UpdateClient
{
    public sealed class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, Result<bool>>
    {
        private readonly IClientRepository _clientRepository;

        public UpdateClientCommandHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<Result<bool>> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var client = await _clientRepository.GetByIdAsync(request.Id);

                if (client == null)
                    return Result<bool>.Fail("Client not found");

                // Parse ClientType from string to enum
                if (!Enum.TryParse<ClientType>(request.ClientType, true, out var clientType))
                    return Result<bool>.Fail($"Invalid client type: {request.ClientType}");

                client.Name = request.Name;
                client.Address = request.Address;
                client.Email = request.Email;
                client.Mobile = request.Mobile;
                client.OfficeEmail = request.OfficeEmail;
                client.Phone = request.Phone;
                client.ReferalBy = request.ReferralBy;
                client.RegNo = request.RegNo;
                client.Proprietor = request.Proprietor;
                client.ClientType = clientType;

                await _clientRepository.UpdateAsync(client);
                return Result<bool>.Success(true, "Client updated successfully");
            }
            catch (Exception ex)
            {
                return Result<bool>.Fail($"Error updating client: {ex.Message}");
            }
        }
    }
}
