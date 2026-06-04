using CourtApp.Application.Common;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseDetails
{
    public class CaseClientInfoUpdateCommand : IRequest<Result<Guid>>
    {
        public Guid ClientId { get; set; }
        public Guid CaseId { get; set; }
    }
    public class CaseClientInfoUpdateCommandHandler : IRequestHandler<CaseClientInfoUpdateCommand, Result<Guid>>
    {
        private readonly IUserCaseRepository _Repository;
        private IUnitOfWork _unitOfWork { get; set; }
        public CaseClientInfoUpdateCommandHandler(IUserCaseRepository _Repository, IUnitOfWork _unitOfWork)
        {
            this._Repository = _Repository;
            this._unitOfWork = _unitOfWork;
        }
        public async Task<Result<Guid>> Handle(CaseClientInfoUpdateCommand request, CancellationToken cancellationToken)
        {
            Guid Id = Guid.Empty;
            var entity = await _Repository.GetByIdAsync(request.CaseId);
            if (entity == null)
                return Result<Guid>.Fail($"Case is not found.");
            else
            {
                entity.ClientId = request.CaseId;
                await _Repository.UpdateAsync(entity);
                Id = entity.Id;
            }
            await _unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success(Id);
        }
    }
}
