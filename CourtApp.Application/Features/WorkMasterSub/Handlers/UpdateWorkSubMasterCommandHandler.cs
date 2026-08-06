using CourtApp.Application.Common;
using CourtApp.Application.Features.WorkMasterSub.Commands;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.WorkMasterSub.Handlers
{
    public class UpdateWorkSubMasterCommandHandler : IRequestHandler<UpdateWorkSubMasterCommand, Result<Guid>>
    {
        private readonly IWorkMasterSubRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateWorkSubMasterCommandHandler(IWorkMasterSubRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(UpdateWorkSubMasterCommand request, CancellationToken cancellationToken)
        {
            var existingRecord = await _repository.GetByIdAsync(request.Id);
            if (existingRecord == null)
                return Result<Guid>.Fail("Record does not exist.");

            string normalizedName = request.Name_En.ToUpper().Trim();
            var duplicateRecord = await _repository.Entities
                .Where(e => e.Id != request.Id
                            && e.WorkId == request.WorkId
                            && e.CourtTypeId == request.CourtTypeId
                            && e.Name.ToUpper().Trim() == normalizedName)
                .FirstOrDefaultAsync(cancellationToken);

            if (duplicateRecord != null)
                return Result<Guid>.Fail("Another record with the same name already exists for this Court Type and Work Master.");

            existingRecord.Name = normalizedName;
            existingRecord.Code = request.Abbreviation;
            existingRecord.WorkId = request.WorkId;
            existingRecord.CourtTypeId = request.CourtTypeId;

            await _repository.UpdateAsync(existingRecord);
            await _unitOfWork.Commit(cancellationToken);

            return Result<Guid>.Success(existingRecord.Id);
        }
    }
}
