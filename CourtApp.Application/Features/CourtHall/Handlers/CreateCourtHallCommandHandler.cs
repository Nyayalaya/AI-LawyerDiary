using CourtApp.Application.Common;
using CourtApp.Application.Features.CourtHall.Commands;
using CourtApp.Application.Features.CourtHall.Interfaces;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.Masters;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtHall.Handlers
{
    public class CreateCourtHallCommandHandler : IRequestHandler<CreateCourtHallCommand, Result<Guid>>
    {
        private readonly ICourtHallRepository repository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCourtHallCommandHandler(ICourtHallRepository repository, IUnitOfWork _unitOfWork)
        {
            this.repository = repository;
            this._unitOfWork = _unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateCourtHallCommand request, CancellationToken cancellationToken)
        {
            if (request.Halls == null || request.Halls.Count == 0)
                return Result<Guid>.Fail("Court halls are not supplied!");

            var insertedId = Guid.Empty;

            foreach (var hall in request.Halls)
            {
                // Normalize to avoid null reference
                var normalizedName = hall.Name?.Trim().ToLower();

                // Check for duplicate name within same complex
                var nameExists = await repository.Entities
                    .AnyAsync(w =>
                        w.Name.Trim().ToLower() == normalizedName &&
                        w.CourtComplexId == request.CourtComplexId,
                        cancellationToken);

                if (nameExists)
                {
                    return Result<Guid>.Fail($"Court hall with name '{hall.Name}' already exists in this complex.");
                }

                var newEntity = new CourtHallEntity
                {
                    Name = hall.Name?.Trim(),
                    JudgeName = hall.JudgeName?.Trim(),
                    RoomNumber = hall.RoomNumber?.Trim(),
                    CourtComplexId = request.CourtComplexId
                };

                await repository.InsertAsync(newEntity);
                insertedId = newEntity.Id;
            }

            // Commit after all inserts
            await _unitOfWork.Commit(cancellationToken);

            return Result<Guid>.Success(insertedId);
        }
    }
}
