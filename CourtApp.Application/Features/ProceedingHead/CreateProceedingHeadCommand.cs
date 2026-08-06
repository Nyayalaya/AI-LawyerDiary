using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CourtApp.Domain.Entities.Masters;

namespace CourtApp.Application.Features.ProceedingHead
{
    public class CreateProceedingHeadCommand : IRequest<Result<Guid>>
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
    public class CreateProceedingHeadCommandHandler : IRequestHandler<CreateProceedingHeadCommand, Result<Guid>>
    {
        private readonly IProceedingHeadRepository _Repository;
        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork { get; set; }
        public CreateProceedingHeadCommandHandler(IProceedingHeadRepository _Repository, IMapper _mapper, IUnitOfWork _unitOfWork)
        {
            this._mapper = _mapper;
            this._Repository = _Repository;
            this._unitOfWork = _unitOfWork;
        }
        public async Task<Result<Guid>> Handle(CreateProceedingHeadCommand request, CancellationToken cancellationToken)
        {
            var dt = _Repository.Entities
                .Where(w => w.Name.Equals(request.Name) || w.Code.Equals(request.Code))
                .FirstOrDefault();
            if (dt != null) return Result<Guid>.Fail("Code or Name is already exist");
            var entity = _mapper.Map<ProceedingTypeEntity>(request);
            await _Repository.InsertAsync(entity);
            await _unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success(entity.Id);
        }
    }
}
