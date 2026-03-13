using CourtApp.Application.Common;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Cadre
{
    public class DeleteCadreCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
    }
    public class DeleteCadreCommandHandler : IRequestHandler<DeleteCadreCommand, Result<Guid>>
    {
        private readonly ICadreMasterRepository repository;
        private readonly IUnitOfWork unitOfWork;
        public DeleteCadreCommandHandler(ICadreMasterRepository repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<Guid>> Handle(DeleteCadreCommand request, CancellationToken cancellationToken)
        {
            var detail = await repository.GetByIdAsync(request.Id);
            if (detail == null) return Result<Guid>.Fail("Record is not exist");
            else
            {
                await repository.DeleteAsync(detail);
                await unitOfWork.Commit(cancellationToken);
                return Result<Guid>.Success(detail.Id);
            }
        }
    }
}
