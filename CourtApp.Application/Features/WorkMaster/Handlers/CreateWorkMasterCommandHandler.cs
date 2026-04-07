using AutoMapper;
using CourtApp.Application.Common;
using CourtApp.Application.Features.WorkMaster.Commands;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.Masters;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.WorkMaster.Handlers
{
    public class CreateWorkMasterCommandHandler : IRequestHandler<CreateWorkMasterCommand, Result<Guid>>
    {
        private readonly IWorkMasterRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateWorkMasterCommandHandler(IWorkMasterRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateWorkMasterCommand request, CancellationToken cancellationToken)
        {
            var existingWork = _repository.Entities
                .Where(x => x.Name.Equals(request.Name_En) && x.CourtTypeId == request.CourtTypeId)
                .FirstOrDefault();

            if (existingWork != null)
                return Result<Guid>.Fail("This work type already exists for the selected court type.");

            var entity = _mapper.Map<WorkTypeEntity>(request);
            await _repository.InsertAsync(entity);
            await _unitOfWork.Commit(cancellationToken);

            return Result<Guid>.Success(entity.Id);
        }
    }
}
