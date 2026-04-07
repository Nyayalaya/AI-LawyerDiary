using CourtApp.Application.Common;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.Masters;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtComplex.Handlers
{
    public class CreateCourtComplexCommandHandler : IRequestHandler<CreateCourtComplexCommand, Result<Guid>>
    {
        private readonly ICourtComplexRepository repository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCourtComplexCommandHandler(ICourtComplexRepository repository, IUnitOfWork _unitOfWork)
        {
            this.repository = repository;
            this._unitOfWork = _unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateCourtComplexCommand request, CancellationToken cancellationToken)
        {
            if (request.Complexes == null || request.Complexes.Count == 0)
                return Result<Guid>.Fail("Court complexes are not supplied!");

            var insertedId = Guid.Empty;

            foreach (var complex in request.Complexes)
            {
                // Normalize the name to avoid null reference and trim/ToLower
                var normalizedName = complex.Name?.Trim().ToLower();
                var normalizedCode = complex.Code?.Trim().ToLower();

                var exists = await repository.Entities
                    .AnyAsync(w =>
                        w.Name.Trim().ToLower() == normalizedName &&
                        w.StateId == request.StateId &&
                        w.CourtDistrictId == request.CourtDistrictId,
                        cancellationToken);

                if (exists)
                {
                    return Result<Guid>.Fail($"Record already exists: {complex.Name}");
                }

                if (!string.IsNullOrWhiteSpace(normalizedCode))
                {
                    var codeExists = await repository.Entities
                        .AnyAsync(w =>
                            w.Code.Trim().ToLower() == normalizedCode &&
                            w.StateId == request.StateId,
                            cancellationToken);

                    if (codeExists)
                    {
                        return Result<Guid>.Fail($"Code already exists: {complex.Code}");
                    }
                }

                var newEntity = new CourtComplexEntity
                {
                    Name = complex.Name?.Trim(),
                    Code = complex.Code?.Trim(),
                    StateId = request.StateId,
                    CourtDistrictId = request.CourtDistrictId
                };

                await repository.InsertAsync(newEntity);
                insertedId = newEntity.Id;
            }

            // Commit after all inserts (only once)
            await _unitOfWork.Commit(cancellationToken);

            return Result<Guid>.Success(insertedId);
        }
    }
}
