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

namespace CourtApp.Application.Features.Location.Handlers
{
    public class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand, Result<Guid>>
    {
        private readonly ILocationRepository _repository;
        private readonly ILocationCacheRepository _cacheRepository;

        public CreateLocationCommandHandler(
            ILocationRepository repository,
            ILocationCacheRepository cacheRepository)
        {
            _repository = repository;
            _cacheRepository = cacheRepository;
        }

        public async Task<Result<Guid>> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
        {
            var locations = new List<LocationEntity>();

            foreach (var detail in request.Locations)
            {
                var nameNormalized = detail.Name?.Trim().ToLower() ?? string.Empty;

                var existingByName = await _repository.GetByNameAndStateAsync(nameNormalized, request.StateId);
                if (existingByName != null)
                    return Result<Guid>.Fail($"Location with name '{detail.Name}' already exists in this state");

                var location = new LocationEntity
                {
                    Id = Guid.NewGuid(),
                    Name = detail.Name?.Trim(),
                    StateId = request.StateId,
                    Type = (CourtApp.Domain.Enums.LocationType)detail.Type,
                    ParentLocationId = detail.ParentLocationId
                };

                locations.Add(location);
            }

            foreach (var location in locations)
            {
                await _repository.AddAsync(location);
            }

            await _cacheRepository.RemoveAsync(Guid.Empty);

            return Result<Guid>.Success(locations.First().Id);
        }
    }
}
