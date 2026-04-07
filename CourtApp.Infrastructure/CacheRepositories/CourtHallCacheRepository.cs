using CourtApp.Application.CacheKeys;
using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using CourtApp.Application.Features.CourtHall.DTOs;
using CourtApp.Application.Features.CourtHall.Interfaces;

namespace CourtApp.Infrastructure.CacheRepositories
{
    public class CourtHallCacheRepository : ICourtHallCacheRepository
    {
        private readonly ICourtHallRepository _repository;
        private readonly IDistributedCache _distributedCache;
        private readonly IMapper _mapper;

        public CourtHallCacheRepository(
            ICourtHallRepository repository,
            IDistributedCache distributedCache,
            IMapper mapper)
        {
            this._repository = repository;
            this._distributedCache = distributedCache;
            this._mapper = mapper;
        }

        public async Task<List<CourtHallResponse>> GetAllAsync()
        {
            var cacheKey = CourtHallCacheKeys.CourtHallKey;
            var cachedData = await _distributedCache.GetStringAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedData))
            {
                return JsonSerializer.Deserialize<List<CourtHallResponse>>(cachedData);
            }

            var data = await _repository.GetListAsync();
            var mappedData = _mapper.Map<List<CourtHallResponse>>(data);

            var options = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromHours(24));

            await _distributedCache.SetStringAsync(cacheKey, JsonSerializer.Serialize(mappedData), options);

            return mappedData;
        }

        public async Task<List<CourtHallResponse>> GetByCourtComplexIdAsync(Guid courtComplexId)
        {
            var cacheKey = string.Format(CourtHallCacheKeys.CourtHallByComplexKey, courtComplexId);
            var cachedData = await _distributedCache.GetStringAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedData))
            {
                return JsonSerializer.Deserialize<List<CourtHallResponse>>(cachedData);
            }

            var data = await _repository.GetByCourtComplexIdAsync(courtComplexId);
            var mappedData = _mapper.Map<List<CourtHallResponse>>(data);

            var options = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromHours(24));

            await _distributedCache.SetStringAsync(cacheKey, JsonSerializer.Serialize(mappedData), options);

            return mappedData;
        }

        public async Task RemoveAsync()
        {
            await _distributedCache.RemoveAsync(CourtHallCacheKeys.CourtHallKey);
        }
    }
}
