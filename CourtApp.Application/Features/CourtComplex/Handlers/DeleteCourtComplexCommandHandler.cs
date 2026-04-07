using CourtApp.Application.Common;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtComplex.Handlers
{
    public class DeleteCourtComplexCommandHandler : IRequestHandler<DeleteCourtComplexCommand, Result<Guid>>
    {
        private readonly ICourtComplexRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCourtComplexCommandHandler(ICourtComplexRepository repository, IUnitOfWork unitOfWork)
        {
            this._repository = repository;
            this._unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(DeleteCourtComplexCommand cmd, CancellationToken cancellationToken)
        {
            var detail = await _repository.GetByIdAsync(cmd.Id);
            if (detail == null)
                return Result<Guid>.Fail("Record does not exist.");

            await _repository.DeleteAsync(detail);
            await _unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success(detail.Id);
        }
    }
}
