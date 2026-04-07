using CourtApp.Application.CacheKeys;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.Masters;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.WorkMasterSub.Services
{
    /// <summary>
    /// Implementation of Work Master Sub cache service
    /// Manages caching of Work Master Sub data using distributed cache (Redis)
    /// Reduces database load for frequently accessed data
    /// </summary>
    public class WorkMasterSubCacheService : IWorkMasterSubCacheService
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IWorkMasterSubService _service;
        private readonly ILogger<WorkMasterSubCacheService> _logger;
        private readonly DistributedCacheEntryOptions _cacheOptions;
        private const string CACHE_KEY_PREFIX = "workmastersub_";
        private const string CACHE_KEY_LIST = "workmastersub_list";

        public WorkMasterSubCacheService(
            IDistributedCache distributedCache,
            IWorkMasterSubService service,
            ILogger<WorkMasterSubCacheService> logger)
        {
            _distributedCache = distributedCache;
            _service = service;
            _logger = logger;

            // Set cache options: 24 hours expiration
            _cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24)
            };
        }

        /// <summary>
        /// Retrieves all Work Master Subs from cache or database
        /// Caches the entire list for 24 hours
        /// </summary>
        public async Task<List<WorksEntity>> GetCachedListAsync()
        {
            try
            {
                var cacheKey = CACHE_KEY_LIST;

                // Try to get from cache
                var cachedData = await _distributedCache.GetStringAsync(cacheKey);
                if (!string.IsNullOrEmpty(cachedData))
                {
                    _logger.LogInformation("Work Master Sub list retrieved from cache");
                    return JsonSerializer.Deserialize<List<WorksEntity>>(cachedData);
                }

                // If not in cache, get from database
                var data = await _service.GetListAsync();
                if (data != null && data.Count > 0)
                {
                    // Cache the data
                    var serializedData = JsonSerializer.Serialize(data);
                    await _distributedCache.SetStringAsync(cacheKey, serializedData, _cacheOptions);
                    _logger.LogInformation("Work Master Sub list cached for 24 hours");
                }

                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Work Master Sub list from cache");
                // Fallback to database
                return await _service.GetListAsync();
            }
        }

        /// <summary>
        /// Retrieves a specific Work Master Sub by ID from cache or database
        /// </summary>
        public async Task<WorksEntity> GetByIdAsync(Guid workSubId)
        {
            try
            {
                if (workSubId == Guid.Empty)
                    throw new ArgumentException("Work Master Sub ID cannot be empty.", nameof(workSubId));

                var cacheKey = $"{CACHE_KEY_PREFIX}{workSubId}";

                // Try to get from cache
                var cachedData = await _distributedCache.GetStringAsync(cacheKey);
                if (!string.IsNullOrEmpty(cachedData))
                {
                    _logger.LogInformation($"Work Master Sub {workSubId} retrieved from cache");
                    return JsonSerializer.Deserialize<WorksEntity>(cachedData);
                }

                // If not in cache, get from database
                var data = await _service.GetByIdAsync(workSubId);
                if (data != null)
                {
                    // Cache the individual record
                    var serializedData = JsonSerializer.Serialize(data);
                    await _distributedCache.SetStringAsync(cacheKey, serializedData, _cacheOptions);
                    _logger.LogInformation($"Work Master Sub {workSubId} cached for 24 hours");
                }

                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving Work Master Sub {workSubId} from cache");
                // Fallback to database
                return await _service.GetByIdAsync(workSubId);
            }
        }

        /// <summary>
        /// Invalidates all Work Master Sub cache entries
        /// Called after Create, Update, or Delete operations
        /// </summary>
        public async Task InvalidateCacheAsync()
        {
            try
            {
                // Clear the list cache
                await _distributedCache.RemoveAsync(CACHE_KEY_LIST);
                _logger.LogInformation("Work Master Sub cache invalidated");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error invalidating Work Master Sub cache");
            }
        }
    }
}
