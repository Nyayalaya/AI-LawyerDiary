using CourtApp.Application.Common;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.WorkMasterSub
{
    public class DeleteWorkSubMstCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
    }
    public class DeleteWorkSubMstCommandHandler : IRequestHandler<DeleteWorkSubMstCommand, Result<Guid>>
    {
        private readonly IWorkMasterSubRepository repository;
        private readonly IUnitOfWork unitOfWork;
        public DeleteWorkSubMstCommandHandler(IWorkMasterSubRepository repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<Guid>> Handle(DeleteWorkSubMstCommand request, CancellationToken cancellationToken)
        {
            var detail = await repository.GetByIdAsync(request.Id);
            await repository.DeleteAsync(detail);
            await unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success(detail.Id);
        }
    }
}
