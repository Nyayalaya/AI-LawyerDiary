using AutoMapper;
using CourtApp.Application.Common;
using CourtApp.Application.Features.Clients.Commands;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Clients.Handlers
{
    public class ClientUpdateCommandHandler : IRequestHandler<ClientUpdateCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClientRepository _clientRepository;
        public ClientUpdateCommandHandler(IClientRepository _clientRepository,
            IUnitOfWork unitOfWork)
        {
            this._clientRepository = _clientRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(ClientUpdateCommand cmd, CancellationToken cancellationToken)
        {
            var detail = await _clientRepository.GetByIdAsync(cmd.Id);
            if (detail == null)
                return Result<string>.Fail($"Client Not Found.");

            detail.ClientType = cmd.ClientType;
            detail.Name = cmd.Name;
            detail.Email = cmd.Email;
            detail.OfficeEmail = cmd.OfficeEmail;
            detail.Phone = cmd.Phone;
            detail.Mobile = cmd.Mobile;
            detail.ReferalBy = cmd.ReferalBy;
            detail.Properiter = cmd.Properiter;
            detail.RegNo = cmd.RegNo;
            detail.Address = cmd.Address;
            await _clientRepository.UpdateAsync(detail);
            await _unitOfWork.Commit(cancellationToken);
            return Result<string>.Success("Client detail has been successfully updated!");

        }
    }
}
