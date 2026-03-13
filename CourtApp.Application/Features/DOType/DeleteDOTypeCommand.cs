using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace CourtApp.Application.Features.DOType
{
    public class DeleteDOTypeCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
    }
    public class DeleteDOTypeCommandHandler : IRequestHandler<DeleteDOTypeCommand, Result<Guid>>
    {
        private readonly IDOTypeRepository _Repo;       
        private IUnitOfWork _uow{ get; set; }
        public DeleteDOTypeCommandHandler(IDOTypeRepository _Repo, IUnitOfWork _uow)
        {
            this._Repo = _Repo;
            this._uow = _uow;
        }
        public async Task<Result<Guid>> Handle(DeleteDOTypeCommand request, CancellationToken cancellationToken)
        {
            var enity = await _Repo.GetByIdAsync(request.Id);
            await _Repo.DeleteAsync(enity);
            await _uow.Commit(cancellationToken);
            return Result<Guid>.Success(enity.Id);
        }
    }
}
