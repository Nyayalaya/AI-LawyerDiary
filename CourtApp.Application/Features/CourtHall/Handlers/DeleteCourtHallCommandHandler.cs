using CourtApp.Application.Common;
using CourtApp.Application.Features.CourtHall.Commands;
using CourtApp.Application.Features.CourtHall.Interfaces;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtHall.Handlers
{
    public class DeleteCourtHallCommandHandler : IRequestHandler<DeleteCourtHallCommand, Result<Guid>>
    {
        private readonly ICourtHallRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCourtHallCommandHandler(ICourtHallRepository repository, IUnitOfWork unitOfWork)
        {
            this._repository = repository;
            this._unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(DeleteCourtHallCommand cmd, CancellationToken cancellationToken)
        {
            var detail = await _repository.GetByIdAsync(cmd.Id);
            if (detail == null)
                return Result<Guid>.Fail("Court hall record does not exist.");

            await _repository.DeleteAsync(detail);
            await _unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success(detail.Id);
        }
    }
}
