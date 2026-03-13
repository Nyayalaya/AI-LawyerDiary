using System;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Application.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseKinds.Commands
{
   public class DeleteCaseKindCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public DeleteCaseKindCommand()
        {

        }
        
    }

    public class DeleteCaseKindCommandHandler : IRequestHandler<DeleteCaseKindCommand, Result<Guid>>
    {
        private readonly ICaseKindRepository _Repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCaseKindCommandHandler(ICaseKindRepository _Repository, IUnitOfWork unitOfWork)
        {
            this._Repository = _Repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(DeleteCaseKindCommand command, CancellationToken cancellationToken)
        {
            var detail = await _Repository.GetByIdAsync(command.Id);
            await _Repository.DeleteAsync(detail);
            await _unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success(detail.Id);
        }
    }
}
