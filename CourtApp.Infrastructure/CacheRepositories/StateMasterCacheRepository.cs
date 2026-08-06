using AutoMapper;
using CourtApp.Application.CacheKeys;
using CourtApp.Application.Common;
using CourtApp.Application.Constants;
using CourtApp.Application.Features.State.Query;
using CourtApp.Application.Features.State.Services;
using CourtApp.Domain.Entities;
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
    public class StateMasterCacheRepository : IStateCacheRepository
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IStateMasterRepository _repository;
        private readonly ILogger<StateMasterCacheRepository> logger;
        private readonly DistributedCacheEntryOptions _cacheOptions;
        private readonly IMapper _mapper;
        public StateMasterCacheRepository(
            IDistributedCache _distributedCache,
            IStateMasterRepository _repository,
            ILogger<StateMasterCacheRepository> _logger,
            IMapper _mapper
            )
        {
            this._distributedCache=_distributedCache;
            this._repository=_repository;
            logger = _logger;
            this._mapper = _mapper;
            _cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24)
            };
        }

        public async Task<List<StateEntity>> GetCachedListAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                string cacheKey = CacheKeys.List<StateEntity>();

                // Try to get from cache
                var cachedData = await _distributedCache.GetStringAsync(cacheKey, cancellationToken);
                if (!string.IsNullOrEmpty(cachedData))
                {
                    var stateData = JsonSerializer.Deserialize<List<StateEntity>>(cachedData);
                    logger.LogInformation($"State list retrieved from cache ({stateData?.Count ?? 0} items)");
                    return stateData ?? new List<StateEntity>();
                }

                // If not in cache, fetch from database
                var courtTypes = await _repository.Entities
                    .AsNoTracking()
                    .OrderBy(x => x.Name)
                    .ToListAsync(cancellationToken);

                if (courtTypes.Count > 0)
                {
                    // Cache the result
                    var serialized = JsonSerializer.Serialize(courtTypes);
                    await _distributedCache.SetStringAsync(cacheKey, serialized, _cacheOptions, cancellationToken);
                    logger.LogInformation($"State master list cached successfully ({courtTypes.Count} items)");
                }

                return courtTypes;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error in GetCachedListAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<List<GetStateMasterResponse>> GetCachedMappedListAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var states = await GetCachedListAsync(cancellationToken);
                var mappedList = _mapper.Map<List<GetStateMasterResponse>>(states);
                return mappedList;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error in GetCachedMappedListAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<List<DropdownInt>> GetDropdownAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                string cacheKey = CourtTypeCacheKeys.DropdownKey;

                // Try to get from cache
                var cachedData = await _distributedCache.GetStringAsync(cacheKey, cancellationToken);
                if (!string.IsNullOrEmpty(cachedData))
                {
                    var dropdownList = JsonSerializer.Deserialize<List<DropdownInt>>(cachedData);
                    logger.LogInformation($"State dropdown retrieved from cache ({dropdownList?.Count ?? 0} items)");
                    return dropdownList ?? new List<DropdownInt>();
                }

                // If not in cache, fetch from database
                var courtTypes = await _repository.Entities
                    .AsNoTracking()
                    .OrderBy(x => x.Name)
                    .Select(x => new DropdownInt
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Code = x.Code
                    })
                    .ToListAsync(cancellationToken);

                if (courtTypes.Count > 0)
                {
                    // Cache the result
                    var serialized = JsonSerializer.Serialize(courtTypes);
                    await _distributedCache.SetStringAsync(cacheKey, serialized, _cacheOptions, cancellationToken);
                    logger.LogInformation($"State dropdown cached successfully ({courtTypes.Count} items)");
                }

                return courtTypes;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error in GetDropdownAsync: {ex.Message}");
                throw;
            }
        }
    }
}