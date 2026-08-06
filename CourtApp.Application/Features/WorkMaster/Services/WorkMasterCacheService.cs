using CourtApp.Application.CacheKeys;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.Masters;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.WorkMaster.Services
{
    /// <summary>
    /// Implementation of Work Master cache service
    /// Manages caching of Work Master data using distributed cache (Redis)
    /// Reduces database load for frequently accessed data
    /// </summary>
    public class WorkMasterCacheService : IWorkMasterCacheService
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IWorkMasterService _service;
        private readonly ILogger<WorkMasterCacheService> _logger;
        private readonly DistributedCacheEntryOptions _cacheOptions;
        private const string CACHE_KEY_PREFIX = "workmaster_";
        private const string CACHE_KEY_LIST = "workmaster_list";

        public WorkMasterCacheService(
            IDistributedCache distributedCache,
            IWorkMasterService service,
            ILogger<WorkMasterCacheService> logger)
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
        /// Retrieves all Work Masters from cache or database
        /// Caches the entire list for 24 hours
        /// </summary>
        public async Task<List<WorkTypeEntity>> GetCachedListAsync()
        {
            try
            {
                var cacheKey = CACHE_KEY_LIST;

                // Try to get from cache
                var cachedData = await _distributedCache.GetStringAsync(cacheKey);
                if (!string.IsNullOrEmpty(cachedData))
                {
                    _logger.LogInformation("Work Master list retrieved from cache");
                    return JsonSerializer.Deserialize<List<WorkTypeEntity>>(cachedData);
                }

                // If not in cache, get from database
                var data = await _service.GetListAsync();
                if (data != null && data.Count > 0)
                {
                    // Cache the data
                    var serializedData = JsonSerializer.Serialize(data);
                    await _distributedCache.SetStringAsync(cacheKey, serializedData, _cacheOptions);
                    _logger.LogInformation("Work Master list cached for 24 hours");
                }

                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Work Master list from cache");
                // Fallback to database
                return await _service.GetListAsync();
            }
        }

        /// <summary>
        /// Retrieves a specific Work Master by ID from cache or database
        /// </summary>
        public async Task<WorkTypeEntity> GetByIdAsync(Guid workMasterId)
        {
            try
            {
                if (workMasterId == Guid.Empty)
                    throw new ArgumentException("Work Master ID cannot be empty.", nameof(workMasterId));

                var cacheKey = $"{CACHE_KEY_PREFIX}{workMasterId}";

                // Try to get from cache
                var cachedData = await _distributedCache.GetStringAsync(cacheKey);
                if (!string.IsNullOrEmpty(cachedData))
                {
                    _logger.LogInformation($"Work Master {workMasterId} retrieved from cache");
                    return JsonSerializer.Deserialize<WorkTypeEntity>(cachedData);
                }

                // If not in cache, get from database
                var data = await _service.GetByIdAsync(workMasterId);
                if (data != null)
                {
                    // Cache the individual record
                    var serializedData = JsonSerializer.Serialize(data);
                    await _distributedCache.SetStringAsync(cacheKey, serializedData, _cacheOptions);
                    _logger.LogInformation($"Work Master {workMasterId} cached for 24 hours");
                }

                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving Work Master {workMasterId} from cache");
                // Fallback to database
                return await _service.GetByIdAsync(workMasterId);
            }
        }

        /// <summary>
        /// Invalidates all Work Master cache entries
        /// Called after Create, Update, or Delete operations
        /// </summary>
        public async Task InvalidateCacheAsync()
        {
            try
            {
                // Clear the list cache
                await _distributedCache.RemoveAsync(CACHE_KEY_LIST);
                _logger.LogInformation("Work Master cache invalidated");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error invalidating Work Master cache");
            }
        }
    }
}
