using CourtApp.Application.Common;
using CourtApp.Application.Interfaces.CacheRepositories;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Location.Handlers
{
    public class UpdateLocationCommandHandler : IRequestHandler<UpdateLocationCommand, Result<Guid>>
    {
        private readonly ILocationRepository _repository;
        private readonly ILocationCacheRepository _cacheRepository;

        public UpdateLocationCommandHandler(
            ILocationRepository repository,
            ILocationCacheRepository cacheRepository)
        {
            _repository = repository;
            _cacheRepository = cacheRepository;
        }

        public async Task<Result<Guid>> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
        {
            var location = await _repository.GetByIdAsync(request.Id);
            if (location == null)
                return Result<Guid>.Fail("Location not found");

            var nameNormalized = request.Name?.Trim().ToLower() ?? string.Empty;

            if (location.Name?.ToLower() != nameNormalized)
            {
                var existingByName = await _repository.GetByNameAndStateAsync(nameNormalized, location.StateId);
                if (existingByName != null && existingByName.Id != request.Id)
                    return Result<Guid>.Fail($"Location with name '{request.Name}' already exists");
            }

            location.Name = request.Name?.Trim();
            location.Type = (CourtApp.Domain.Enums.LocationType)request.Type;
            location.ParentLocationId = request.ParentLocationId;

            _repository.Update(location);

            await _cacheRepository.RemoveAsync(request.Id);

            return Result<Guid>.Success(request.Id);
        }
    }
}
