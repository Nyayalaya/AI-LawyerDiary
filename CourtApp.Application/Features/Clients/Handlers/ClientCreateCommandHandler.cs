using AutoMapper;
using CourtApp.Application.Common;
using CourtApp.Application.Features.Clients.Commands;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Application.Interfaces.Repositories.Common;
using CourtApp.Domain.Entities.LawyerDiary;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Clients.Handlers
{
    public class ClientCreateCommandHandler : IRequestHandler<ClientCreateCommand, Result<string>>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork { get; set; }
        public ClientCreateCommandHandler(
            IClientRepository _clientRepository,
            IUnitOfWork unitOfWork, IMapper mapper
            )
        {
            this._clientRepository = _clientRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        public async Task<Result<string>> Handle(ClientCreateCommand request, CancellationToken cancellationToken)
        {
            var detail = _clientRepository.Clients
                .Where(w => w.CreatedBy.Equals(request.UserId)
                        && w.Name.Trim().ToLower().Equals(request.Name.Trim().ToLower())
                        && w.Mobile.Equals(request.Mobile)
                        && w.Address.Trim().ToLower().Equals(request.Address.Trim().ToLower())
                ).FirstOrDefault();
            if (detail != null)
                return await Result<string>
                    .FailAsync($"Error! The client is already exist with client name {request.Name}, mobile {request.Mobile}, and address {request.Address}");

            var entity = _mapper.Map<ClientEntity>(request);
            await _clientRepository.InsertAsync(entity);
            await _unitOfWork.Commit(cancellationToken);
            return await Result<string>.SuccessAsync($"Client {request.Name} is created successfully!");
        }
    }
}
