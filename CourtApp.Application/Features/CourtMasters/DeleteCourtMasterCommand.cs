using CourtApp.Application.Common;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtMasters.Command
{
    public class DeleteCourtMasterCommand:IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
    }
    public class DeleteCourtMasterCommandHandler : IRequestHandler<DeleteCourtMasterCommand, Result<Guid>>
    {
        private readonly ICourtMasterRepository repository;
        private IUnitOfWork _unitOfWork;
        public DeleteCourtMasterCommandHandler(ICourtMasterRepository repository, IUnitOfWork _unitOfWork)
        {
            this.repository = repository;
            this._unitOfWork = _unitOfWork;
        }
        public async Task<Result<Guid>> Handle(DeleteCourtMasterCommand request, CancellationToken cancellationToken)
        {
            var detail = await repository.GetByIdAsync(request.Id);
            await repository.DeleteAsync(detail);
            await _unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success(detail.Id);
        }
    }
}
