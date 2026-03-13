using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Application.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace CourtApp.Application.Features.Clients.Commands
{
    public class DeleteClientCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
    }
    public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand, Result<Guid>>
    {
        private readonly IClientRepository _Repository;
        private readonly IUserCaseRepository _UserCaseRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteClientCommandHandler(IClientRepository _Repository, IUnitOfWork unitOfWork, IUserCaseRepository userCaseRepository)
        {
            this._Repository = _Repository;
            _unitOfWork = unitOfWork;
            _UserCaseRepository = userCaseRepository;
        }

        public async Task<Result<Guid>> Handle(DeleteClientCommand command, CancellationToken cancellationToken)
        {
            var CaseInfo = _UserCaseRepository.Entites.Where(c => c.ClientId == command.Id).FirstOrDefault();
            if (CaseInfo != null) return Result<Guid>.Fail("The client is attached with the case "+ CaseInfo.FirstTitle + ", before deleting please detached first! ");
            var client = await _Repository.GetByIdAsync(command.Id);
            await _Repository.DeleteAsync(client);
            await _unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success(client.Id);
        }
    }
}