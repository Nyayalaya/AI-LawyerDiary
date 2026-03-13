using CourtApp.Application.Common;
using CourtApp.Application.Features.CourtComplex;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.ProceedingHead
{
    public class DeleteProceedingHeadCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteProceedingHeadCommandHandler : IRequestHandler<DeleteProceedingHeadCommand, Result<Guid>>
    {
        private readonly IProceedingHeadRepository _Repository;
        private IUnitOfWork _unitOfWork { get; set; }
        public DeleteProceedingHeadCommandHandler(IProceedingHeadRepository _Repository, IUnitOfWork _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
           this._Repository = _Repository;
        }
        public async Task<Result<Guid>> Handle(DeleteProceedingHeadCommand request, CancellationToken cancellationToken)
        {
            var courtType = await _Repository.GetByIdAsync(request.Id);
            if (courtType == null)
                return Result<Guid>.Fail($"Proceeding Head Not Found.");

            await _Repository.DeleteAsync(courtType);
            await _unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success(courtType.Id);
        }
    }
}
//    public class DeleteProceedingHeadCommandHandler : IRequestHandler<DeleteProceedingHeadCommand, Result<Guid>>
//    {
//        private readonly IProceedingHeadRepository _Repository;
//        private IUnitOfWork _unitOfWork { get; set; }
//        public DeleteProceedingHeadCommandHandler(IUnitOfWork unitOfWork, 
//            IProceedingHeadRepository repository)
//        {
//            _unitOfWork = unitOfWork;
//            this._Repository = repository;
//        }
//        public async Task<Result<Guid>> Handle(DeleteProceedingHeadCommand request, CancellationToken cancellationToken)
//        {
//            var courtType = await _Repository.GetByIdAsync(request.Id);
//            if (courtType == null)
//                return Result<Guid>.Fail($"Proceeding Head Not Found.");

//            await _Repository.DeleteAsync(courtType);
//            await _unitOfWork.Commit(cancellationToken);
//            return Result<Guid>.Success(courtType.Id);
//        }
//    }
//}
