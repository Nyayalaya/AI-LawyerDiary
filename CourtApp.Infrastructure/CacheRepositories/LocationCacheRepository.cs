using CourtApp.Application.CacheKeys;
using CourtApp.Application.DTOs.Location;
using CourtApp.Application.Interfaces.CacheRepositories;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.CacheRepositories
{
    public class LocationCacheRepository : ILocationCacheRepository
    {
        private readonly IDistributedCache _distributedCache;

        public LocationCacheRepository(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<LocationResponse> GetAsync(Guid id)
        {
            var cacheKey = LocationCacheKeys.LocationKey(id);
            var cachedData = await _distributedCache.GetStringAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedData))
            {
                try
                {
                    return JsonSerializer.Deserialize<LocationResponse>(cachedData);
                }
                catch
                {
                    await _distributedCache.RemoveAsync(cacheKey);
                    return null;
                }
            }

            return null;
        }

        public async Task<List<LocationResponse>> GetByStateAsync(int stateId)
        {
            var cacheKey = LocationCacheKeys.LocationByStateKey(stateId);
            var cachedData = await _distributedCache.GetStringAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedData))
            {
                try
                {
                    return JsonSerializer.Deserialize<List<LocationResponse>>(cachedData);
                }
                catch
                {
                    await _distributedCache.RemoveAsync(cacheKey);
                    return null;
                }
            }

            return null;
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            var cacheKey = LocationCacheKeys.LocationKey(id);
            await _distributedCache.RemoveAsync(cacheKey);
            return true;
        }
    }
}
