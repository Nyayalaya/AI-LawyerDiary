using CourtApp.Application.Common;
using CourtApp.Application.Interfaces.CacheRepositories;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.Masters;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Court.Handlers
{
    public class CreateCourtCommandHandler : IRequestHandler<CreateCourtCommand, Result<Guid>>
    {
        private readonly ICourtRepository _repository;
        private readonly ICourtCacheRepository _cacheRepository;

        public CreateCourtCommandHandler(
            ICourtRepository repository,
            ICourtCacheRepository cacheRepository)
        {
            _repository = repository;
            _cacheRepository = cacheRepository;
        }

        public async Task<Result<Guid>> Handle(CreateCourtCommand request, CancellationToken cancellationToken)
        {
            var courts = new List<CourtEntity>();

            foreach (var detail in request.Courts)
            {
                var nameNormalized = detail.Name?.Trim().ToLower() ?? string.Empty;

                var existingByName = await _repository.GetByNameAndLocationAsync(nameNormalized, request.LocationId);
                if (existingByName != null)
                    return Result<Guid>.Fail($"Court with name '{detail.Name}' already exists in this location");

                var court = new CourtEntity
                {
                    Id = Guid.NewGuid(),
                    Name = detail.Name?.Trim(),
                    LocationId = request.LocationId,
                    CourtTypeId = detail.CourtTypeId,
                    CourtLevelId = detail.CourtLevelId
                };

                courts.Add(court);
            }

            foreach (var court in courts)
            {
                await _repository.AddAsync(court);
            }

            await _cacheRepository.RemoveAsync(Guid.Empty);

            return Result<Guid>.Success(courts.FirstOrDefault()?.Id ?? Guid.NewGuid());
        }
    }
}
