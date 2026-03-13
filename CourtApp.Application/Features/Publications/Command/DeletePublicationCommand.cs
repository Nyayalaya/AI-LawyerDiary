using CourtApp.Application.Common;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Publications.Command
{
    public class DeletePublicationCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
    }

    public class DeletePublicationCommandHandler : IRequestHandler<DeletePublicationCommand, Result<Guid>>
    {
        private readonly IPublicationRepository _Repository;
        private readonly IUnitOfWork _unitOfWork;
        public DeletePublicationCommandHandler(IPublicationRepository _Repository, IUnitOfWork _unitOfWork)
        {
            this._Repository = _Repository;
            this._unitOfWork = _unitOfWork;
        }
        public async Task<Result<Guid>> Handle(DeletePublicationCommand request, CancellationToken cancellationToken)
        {
            var entity = await _Repository.GetByIdAsync(request.Id);
            await _Repository.DeleteAsync(entity);
            await _unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success(entity.Id);
        }
    }
}
