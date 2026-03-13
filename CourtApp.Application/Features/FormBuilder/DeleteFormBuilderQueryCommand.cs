using CourtApp.Application.Common;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Application.Interfaces.Repositories.FormBuilder;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace CourtApp.Application.Features.FormBuilder
{
    public class DeleteFormBuilderQueryCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
    }
    public class DeleteFormBuilderQueryCommandHandler : IRequestHandler<DeleteFormBuilderQueryCommand, Result<Guid>>
    {
        private readonly IFormBuilderRepository _Repository;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteFormBuilderQueryCommandHandler(IFormBuilderRepository _Repository, IUnitOfWork unitOfWork)
        {
            this._Repository = _Repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<Guid>> Handle(DeleteFormBuilderQueryCommand request, CancellationToken cancellationToken)
        {
            var detail = await _Repository.GetByIdAsync(request.Id);
            await _Repository.DeleteAsync(detail);
            await _unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success(detail.Id);
        }
    }
}
