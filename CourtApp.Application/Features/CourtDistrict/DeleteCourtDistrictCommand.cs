using CourtApp.Application.Common;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtDistrict
{
    public class DeleteCourtDistrictCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
    }
    public class DeleteCourtDistrictCommandHandler : IRequestHandler<DeleteCourtDistrictCommand, Result<Guid>>
    {
        private readonly ICourtDistrictRepository _Repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCourtDistrictCommandHandler(ICourtDistrictRepository _Repository, IUnitOfWork unitOfWork)
        {
            this._Repository = _Repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(DeleteCourtDistrictCommand cmd, CancellationToken cancellationToken)
        {
            var detail = await _Repository.GetByIdAsync(cmd.Id);
            await _Repository.DeleteAsync(detail);
            await _unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success(detail.Id);
        }
    }
}
