using System;
using System.Threading;
using System.Threading.Tasks;
using CourtApp.Application.Common;
using CourtApp.Application.Features.Clients.Commands.DeleteClient;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;

namespace CourtApp.Application.Features.Clients.Handlers.DeleteClient
{
    public sealed class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand, Result<bool>>
    {
        private readonly IClientRepository _clientRepository;

        public DeleteClientCommandHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<Result<bool>> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var client = await _clientRepository.GetByIdAsync(request.Id);

                if (client == null)
                    return Result<bool>.Fail("Client not found");

                await _clientRepository.DeleteAsync(client);
                return Result<bool>.Success(true, "Client deleted successfully");
            }
            catch (Exception ex)
            {
                return Result<bool>.Fail($"Error deleting client: {ex.Message}");
            }
        }
    }
}
