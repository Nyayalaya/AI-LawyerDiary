using CourtApp.Application.Common;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtComplex
{
    
    public class DeleteCourtComplexCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
    }
    public class DeleteCourtComplexCommandCommandHandler : IRequestHandler<DeleteCourtComplexCommand, Result<Guid>>
    {
        private readonly ICourtComplexRepository _Repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCourtComplexCommandCommandHandler(ICourtComplexRepository _Repository, 
            IUnitOfWork unitOfWork)
        {
            this._Repository = _Repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(DeleteCourtComplexCommand cmd, CancellationToken cancellationToken)
        {
            var detail = await _Repository.GetByIdAsync(cmd.Id);
            await _Repository.DeleteAsync(detail);
            await _unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success(detail.Id);
        }
    }
}
