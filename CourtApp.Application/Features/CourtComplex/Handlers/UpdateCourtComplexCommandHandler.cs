using CourtApp.Application.Common;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtComplex.Handlers
{
    public class UpdateCourtComplexCommandHandler : IRequestHandler<UpdateCourtComplexCommand, Result<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICourtComplexRepository repository;

        public UpdateCourtComplexCommandHandler(ICourtComplexRepository repository, IUnitOfWork _unitOfWork)
        {
            this.repository = repository;
            this._unitOfWork = _unitOfWork;
        }

        public async Task<Result<Guid>> Handle(UpdateCourtComplexCommand cmd, CancellationToken cancellationToken)
        {
            // Fetch the record to update
            var existingRecord = await repository.GetByIdAsync(cmd.Id);
            if (existingRecord == null)
                return Result<Guid>.Fail("Record does not exist.");

            // Check for name conflict with other records (excluding the current record)
            var duplicateRecord = await repository.Entities
                .Where(e => e.Id != cmd.Id
                            && e.StateId == cmd.StateId
                            && e.CourtDistrictId == cmd.CourtDistrictId
                            && e.Name.ToLower() == cmd.Name.ToLower().Trim())
                .FirstOrDefaultAsync(cancellationToken);

            if (duplicateRecord != null)
                return Result<Guid>.Fail("Another record with the same name already exists.");

            // Check for name conflict
            var nameExists = await repository.Entities
                .Where(e => e.Id != cmd.Id
                            && e.StateId == cmd.StateId
                            && e.Name.ToLower() == cmd.Name.ToLower().Trim())
                .FirstOrDefaultAsync(cancellationToken);

            if (nameExists != null)
                return Result<Guid>.Fail("Another record with the same name already exists.");

            // Update the entity fields
            existingRecord.StateId = cmd.StateId;
            existingRecord.CourtDistrictId = cmd.CourtDistrictId;
            existingRecord.Name = cmd.Name?.Trim();

            await repository.UpdateAsync(existingRecord);
            await _unitOfWork.Commit(cancellationToken);

            return Result<Guid>.Success(existingRecord.Id);
        }
    }
}
