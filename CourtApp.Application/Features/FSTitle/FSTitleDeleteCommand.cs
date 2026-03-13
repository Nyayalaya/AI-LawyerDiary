using CourtApp.Application.Common;
using CourtApp.Application.Features.DOType;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.FSTitle
{
    public class FSTitleDeleteCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
    }
    public class FSTitleDeleteCommandHandler : IRequestHandler<FSTitleDeleteCommand, Result<Guid>>
    {
        private readonly IFSTitleRepository _Repo;
        private IUnitOfWork _uow { get; set; }
        public FSTitleDeleteCommandHandler(IFSTitleRepository _Repo, IUnitOfWork _uow)
        {
            this._Repo = _Repo;
            this._uow = _uow;
        }
        public async Task<Result<Guid>> Handle(FSTitleDeleteCommand request, CancellationToken cancellationToken)
        {
            var enity = await _Repo.GetByIdAsync(request.Id);
            await _Repo.DeleteAsync(enity);
            await _uow.Commit(cancellationToken);
            return Result<Guid>.Success(enity.Id);
        }
    }
}
