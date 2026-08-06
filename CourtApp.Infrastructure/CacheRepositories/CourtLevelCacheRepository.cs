using AutoMapper;
using CourtApp.Application.CacheKeys;
using CourtApp.Application.Common;
using CourtApp.Application.Constants;
using CourtApp.Application.Features.CourtLevel.Query;
using CourtApp.Application.Features.CourtLevel.Services;
using CourtApp.Domain.Entities.Masters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.CacheRepositories
{
    public class CourtLevelCacheRepository : ICourtLevelCacheRepository
    {
        private readonly IDistributedCache _distributedCache;
        private readonly ICourtLevelMasterRepository _repository;
        private readonly ILogger<CourtLevelCacheRepository> logger;
        private readonly DistributedCacheEntryOptions _cacheOptions;
        private readonly IMapper _mapper;
        public CourtLevelCacheRepository(
            IDistributedCache _distributedCache,
            ICourtLevelMasterRepository _repository,
            ILogger<CourtLevelCacheRepository> _logger,
            IMapper _mapper
            )
        {
            this._distributedCache = _distributedCache;
            this._repository = _repository;
            logger = _logger;
            this._mapper = _mapper;
            _cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24)
            };
        }

        public async Task<List<CourtLevelEntity>> GetCachedListAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                string cacheKey = CacheKeys.List<CourtLevelEntity>();

                // Try to get from cache
                var cachedData = await _distributedCache.GetStringAsync(cacheKey, cancellationToken);
                if (!string.IsNullOrEmpty(cachedData))
                {
                    var courtLevelData = JsonSerializer.Deserialize<List<CourtLevelEntity>>(cachedData);
                    logger.LogInformation($"Court Level list retrieved from cache ({courtLevelData?.Count ?? 0} items)");
                    return courtLevelData ?? new List<CourtLevelEntity>();
                }

                // If not in cache, fetch from database
                var courtLevels = await _repository.Entities
                    .AsNoTracking()
                    .OrderBy(x => x.Name)
                    .ToListAsync(cancellationToken);

                if (courtLevels.Count > 0)
                {
                    // Cache the result
                    var serialized = JsonSerializer.Serialize(courtLevels);
                    await _distributedCache.SetStringAsync(cacheKey, serialized, _cacheOptions, cancellationToken);
                    logger.LogInformation($"Court Level master list cached successfully ({courtLevels.Count} items)");
                }

                return courtLevels;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error in GetCachedListAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<List<GetCourtLevelResponse>> GetCachedMappedListAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var courtLevels = await GetCachedListAsync(cancellationToken);
                var mappedList = _mapper.Map<List<GetCourtLevelResponse>>(courtLevels);
                return mappedList;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error in GetCachedMappedListAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Dropdown>> GetDropdownAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                string cacheKey = CourtLevelCacheKeys.DropdownKey;

                // Try to get from cache
                var cachedData = await _distributedCache.GetStringAsync(cacheKey, cancellationToken);
                if (!string.IsNullOrEmpty(cachedData))
                {
                    var dropdownList = JsonSerializer.Deserialize<List<Dropdown>>(cachedData);
                    logger.LogInformation($"Court Level dropdown retrieved from cache ({dropdownList?.Count ?? 0} items)");
                    return dropdownList ?? new List<Dropdown>();
                }

                // If not in cache, fetch from database
                var courtLevels = await _repository.Entities
                    .AsNoTracking()
                    .OrderBy(x => x.Name)
                    .Select(x => new Dropdown
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Code = x.Code
                    })
                    .ToListAsync(cancellationToken);

                if (courtLevels.Count > 0)
                {
                    // Cache the result
                    var serialized = JsonSerializer.Serialize(courtLevels);
                    await _distributedCache.SetStringAsync(cacheKey, serialized, _cacheOptions, cancellationToken);
                    logger.LogInformation($"Court Level dropdown cached successfully ({courtLevels.Count} items)");
                }

                return courtLevels;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error in GetDropdownAsync: {ex.Message}");
                throw;
            }
        }
    }
}
