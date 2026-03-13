using CourtApp.Application.Common;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.BookMasters.Command
{
    public class DeleteBookMasterCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }

        public class DeleteBrandCommandHandler : IRequestHandler<DeleteBookMasterCommand, Result<Guid>>
        {
            private readonly IBookMasterRepository _Repository;
            private readonly IUnitOfWork _unitOfWork;

            public DeleteBrandCommandHandler(IBookMasterRepository _Repository, IUnitOfWork unitOfWork)
            {
                this._Repository = _Repository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<Guid>> Handle(DeleteBookMasterCommand command, CancellationToken cancellationToken)
            {
                var booktype = await _Repository.GetByIdAsync(command.Id);
                await _Repository.DeleteAsync(booktype);
                await _unitOfWork.Commit(cancellationToken);
                return Result<Guid>.Success(booktype.Id);
            }
        }
    }
}
