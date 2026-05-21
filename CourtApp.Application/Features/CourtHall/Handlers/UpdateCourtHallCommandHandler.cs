using CourtApp.Application.Common;
using CourtApp.Application.Features.CourtHall.Commands;
using CourtApp.Application.Features.CourtHall.Interfaces;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtHall.Handlers
{
    public class UpdateCourtHallCommandHandler : IRequestHandler<UpdateCourtHallCommand, Result<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICourtHallRepository repository;

        public UpdateCourtHallCommandHandler(ICourtHallRepository repository, IUnitOfWork _unitOfWork)
        {
            this.repository = repository;
            this._unitOfWork = _unitOfWork;
        }

        public async Task<Result<Guid>> Handle(UpdateCourtHallCommand cmd, CancellationToken cancellationToken)
        {
            // Fetch the record to update
            var existingRecord = await repository.GetByIdAsync(cmd.Id);
            if (existingRecord == null)
                return Result<Guid>.Fail("Court hall record does not exist.");

            // Check for name conflict with other records (excluding current)
            var normalizedName = cmd.Name?.Trim().ToLower();
            var duplicateRecord = await repository.Entities
                .Where(e => e.Id != cmd.Id
                            && e.CourtComplexId == cmd.CourtComplexId
                            && e.Name.ToLower() == normalizedName)
                .FirstOrDefaultAsync(cancellationToken);

            if (duplicateRecord != null)
                return Result<Guid>.Fail("Another court hall with the same name already exists in this complex.");

            // Update the entity fields
            existingRecord.Name = cmd.Name?.Trim();
            existingRecord.JudgeName = cmd.JudgeName?.Trim();
            existingRecord.RoomNumber = cmd.RoomNumber?.Trim();
            existingRecord.CourtComplexId = cmd.CourtComplexId;

            await repository.UpdateAsync(existingRecord);
            await _unitOfWork.Commit(cancellationToken);

            return Result<Guid>.Success(existingRecord.Id);
        }
    }
}
