using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Application.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace CourtApp.Application.Features.BookTypes.Command
{
    public class DeleteCreateBookTypeCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }


    }
    public class DeleteBrandCommandHandler : IRequestHandler<DeleteCreateBookTypeCommand, Result<Guid>>
    {
        private readonly IBookTypeRepository _Repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBrandCommandHandler(IBookTypeRepository _Repository, IUnitOfWork unitOfWork)
        {
            this._Repository = _Repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(DeleteCreateBookTypeCommand command, CancellationToken cancellationToken)
        {
            var booktype = await _Repository.GetByIdAsync(command.Id);
            await _Repository.DeleteAsync(booktype);
            await _unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success(booktype.Id);
        }
    }
}