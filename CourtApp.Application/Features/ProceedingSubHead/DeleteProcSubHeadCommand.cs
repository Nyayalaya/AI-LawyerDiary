using CourtApp.Application.Common;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.ProceedingSubHead
{
    public class DeleteProcSubHeadCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
    }
    public class DeleteProcSubHeadCommandHandler : IRequestHandler<DeleteProcSubHeadCommand, Result<Guid>>
    {
        private readonly IProceedingSubHeadRepository _Repository;
        private IUnitOfWork _unitOfWork { get; set; }
        public DeleteProcSubHeadCommandHandler(IUnitOfWork unitOfWork, IProceedingSubHeadRepository repository)
        {

            _unitOfWork = unitOfWork;
            this._Repository = repository;
        }
        public async Task<Result<Guid>> Handle(DeleteProcSubHeadCommand request, CancellationToken cancellationToken)
        {
            var detail = await _Repository.GetByIdAsync(request.Id);
            await _Repository.DeleteAsync(detail);
            await _unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success(detail.Id);
        }
    }
}
