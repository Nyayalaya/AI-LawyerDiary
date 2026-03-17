using CourtApp.Application.Common;
using CourtApp.Application.Features.Clients.Commands;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Clients.Handlers
{
    public class ClientDeleteCommandHandler : IRequestHandler<ClientDeleteCommand, Result<string>>
    {
        private readonly IClientRepository _Repository;
        private readonly IUserCaseRepository _UserCaseRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ClientDeleteCommandHandler(IClientRepository _Repository, IUnitOfWork unitOfWork, IUserCaseRepository userCaseRepository)
        {
            this._Repository = _Repository;
            _unitOfWork = unitOfWork;
            _UserCaseRepository = userCaseRepository;
        }

        public async Task<Result<string>> Handle(ClientDeleteCommand command, CancellationToken cancellationToken)
        {
            var CaseInfo = _UserCaseRepository
                .Entites
                .Where(c => c.ClientId == command.Id)
                .FirstOrDefault();
            if (CaseInfo != null)
                return Result<string>.Fail("The client is attached with the case " + CaseInfo.FirstTitle + ", before deleting please detached first! ");
            var client = await _Repository.GetByIdAsync(command.Id);
            await _Repository.DeleteAsync(client);
            await _unitOfWork.Commit(cancellationToken);
            return Result<string>.Success("Client detail has been successfully deleted!");
        }
    }
}
