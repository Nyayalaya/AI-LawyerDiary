using CourtApp.Application.Common;
using CourtApp.Application.Features.WorkMaster.Commands;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.WorkMaster.Handlers
{
    public class UpdateWorkMasterCommandHandler : IRequestHandler<UpdateWorkMasterCommand, Result<Guid>>
    {
        private readonly IWorkMasterRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateWorkMasterCommandHandler(IWorkMasterRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(UpdateWorkMasterCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
                return Result<Guid>.Fail("Work Master not found.");

            var existingWork = _repository.Entities
                .Where(x => x.Id != request.Id 
                    && x.Name.Equals(request.Name_En) 
                    && x.CourtTypeId == request.CourtTypeId)
                .FirstOrDefault();

            if (existingWork != null)
                return Result<Guid>.Fail("Another work type with the same name already exists for the selected court type.");

            entity.Name = request.Name_En;
            entity.Code = request.Abbreviation;
            entity.CourtTypeId = request.CourtTypeId;

            await _repository.UpdateAsync(entity);
            await _unitOfWork.Commit(cancellationToken);

            return Result<Guid>.Success(entity.Id);
        }
    }
}
