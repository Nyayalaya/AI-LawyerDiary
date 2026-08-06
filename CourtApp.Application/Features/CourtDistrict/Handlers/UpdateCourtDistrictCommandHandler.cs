using CourtApp.Application.Common;
using CourtApp.Application.Features.CourtDistrict.Commands;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtDistrict.Handlers
{
    public class UpdateCourtDistrictCommandHandler : IRequestHandler<UpdateCourtDistrictCommand, Result<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICourtDistrictRepository repository;

        public UpdateCourtDistrictCommandHandler(ICourtDistrictRepository repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(UpdateCourtDistrictCommand request, CancellationToken cancellationToken)
        {
            // Fetch the record to update
            var existingRecord = await repository.GetByIdAsync(request.Id);
            if (existingRecord == null)
                return Result<Guid>.Fail("Record does not exist.");

            var normalizedName = request.Name.Trim().ToLower();

            // Check for name conflict with other records (excluding the current record)
            var duplicateByName = await repository.Entities
                .Where(e => e.Id != request.Id
                    && e.StateId == request.StateId
                    && e.Name.ToLower() == normalizedName)
                .FirstOrDefaultAsync(cancellationToken);

            if (duplicateByName != null)
                return Result<Guid>.Fail($"Another record with name '{request.Name}' already exists in this state.");

            existingRecord.Name = request.Name.Trim();
            existingRecord.StateId = request.StateId;
            existingRecord.Languages = request.Languages ?? new List<LangEntity>();

            await repository.UpdateAsync(existingRecord);
            await _unitOfWork.Commit(cancellationToken);

            return Result<Guid>.Success(existingRecord.Id, "Court District updated successfully.");
        }
    }
}