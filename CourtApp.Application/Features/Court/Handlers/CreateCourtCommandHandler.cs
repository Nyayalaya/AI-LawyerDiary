using CourtApp.Application.Common;
using CourtApp.Application.Features.Court.Commands;
using CourtApp.Application.Interfaces.CacheRepositories;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.Masters;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Court.Handlers
{
    public class CreateCourtCommandHandler : IRequestHandler<CreateCourtCommand, Result<string>>
    {
        private readonly ICourtRepository _repository;
        public readonly IUnitOfWork _uou;

        public CreateCourtCommandHandler(
            ICourtRepository repository,
            IUnitOfWork _uou)
        {
            _repository = repository;
            this._uou = _uou;
        }

        public async Task<Result<string>> Handle(CreateCourtCommand request, CancellationToken cancellationToken)
        {
            if (request.Courts == null || !request.Courts.Any())
            {
                return await Result<string>.FailAsync("Court details are required.");
            }

            var duplicateRequestCodes = request.Courts
               .Where(x => !string.IsNullOrWhiteSpace(x.Code))
               .GroupBy(x => x.Code.Trim().ToUpper())
               .Where(g => g.Count() > 1)
               .Select(g => g.Key)
               .ToList();

            if (duplicateRequestCodes.Any())
            {
                return await Result<string>.FailAsync(
                    $"Duplicate court codes found in request: {string.Join(", ", duplicateRequestCodes)}");
            }

            var requestCodes = request.Courts
                .Where(x => !string.IsNullOrWhiteSpace(x.Code))
                .Select(x => x.Code.Trim().ToUpper())
                .ToList();

            // Check existing codes in database
            var existingCodes = await _repository.Entities
                .AsNoTracking()
                .Where(x => requestCodes.Contains(x.Code.ToUpper()))
                .Select(x => x.Code)
                .ToListAsync(cancellationToken);

            if (existingCodes.Any())
            {
                return await Result<string>.FailAsync(
                    $"Court code already exists: {string.Join(", ", existingCodes)}");
            }

            var entities = new List<CourtEntity>();

            foreach (var item in request.Courts)
            {
                var entity = new CourtEntity
                {
                    Id = Guid.NewGuid(),

                    Name = item.Name.Trim(),
                    Code = item.Code?.Trim(),

                    CourtTypeId = request.CourtTypeId,
                    StateId = request.StateId,
                    IsVirtualCourt = request.IsVirtualCourt,

                    CourtDistrictId = item.CourtDistrictId,
                };

                entities.Add(entity);
            }

            await _repository.AddRangeAsync(entities);
            await _uou.Commit(cancellationToken);

            return await Result<string>.SuccessAsync(
                $"{entities.Count} court(s) created successfully.");

        }
    }
}
