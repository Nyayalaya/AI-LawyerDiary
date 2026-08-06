using AutoMapper;
using CourtApp.Application.CacheKeys;
using CourtApp.Application.Common;
using CourtApp.Application.DTOs.Location;
using CourtApp.Application.Interfaces.CacheRepositories;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Location.Handlers
{
    public class GetLocationCacheQueryHandler : IRequestHandler<GetLocationCacheQuery, Result<List<LocationResponse>>>
    {
        private readonly ILocationRepository _repository;
        private readonly ILocationCacheRepository _cacheRepository;
        private readonly IMapper _mapper;

        public GetLocationCacheQueryHandler(
            ILocationRepository repository,
            ILocationCacheRepository cacheRepository,
            IMapper mapper)
        {
            _repository = repository;
            _cacheRepository = cacheRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<LocationResponse>>> Handle(GetLocationCacheQuery request, CancellationToken cancellationToken)
        {
            var cachedData = await _cacheRepository.GetByStateAsync(request.StateId);
            if (cachedData != null && cachedData.Count > 0)
                return Result<List<LocationResponse>>.Success(cachedData);

            var locations = await _repository.GetByStateIdAsync(request.StateId, 1, int.MaxValue);
            var mappedLocations = _mapper.Map<List<LocationResponse>>(locations);

            return Result<List<LocationResponse>>.Success(mappedLocations);
        }
    }
}
