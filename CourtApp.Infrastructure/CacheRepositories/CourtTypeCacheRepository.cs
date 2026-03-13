using AspNetCoreHero.ThrowR;
using AutoMapper;
using CourtApp.Application.CacheKeys;
using CourtApp.Application.Common;
using CourtApp.Application.Features.CourtType.Query;
using CourtApp.Application.Features.CourtType.Services;
using CourtApp.Domain.Entities.LawyerDiary;
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
    public class CourtTypeCacheRepository : ICourtTypeCacheRepository
    {
        private readonly IDistributedCache _distributedCache;
        private readonly ICourtTypeRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CourtTypeCacheRepository> _logger;
        private readonly DistributedCacheEntryOptions _cacheOptions;

        public CourtTypeCacheRepository(
            IDistributedCache distributedCache,
            ICourtTypeRepository repository,
            IMapper mapper,
            ILogger<CourtTypeCacheRepository> logger)
        {
            _distributedCache = distributedCache;
            _repository = repository;
            _mapper = mapper;
            _logger = logger;

            // Set cache options: 24 hours expiration
            _cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24)
            };
        }

        /// <summary>
        /// Get court type by ID from cache or database
        /// </summary>
        public async Task<CourtTypeEntity> GetByIdAsync(Guid courtTypeId, CancellationToken cancellationToken = default)
        {
            try
            {
                if (courtTypeId == Guid.Empty)
                    throw new ArgumentException("Court type ID cannot be empty.", nameof(courtTypeId));

                string cacheKey = CourtTypeCacheKeys.GetKey(courtTypeId);

                // Try to get from cache
                var cachedData = await _distributedCache.GetStringAsync(cacheKey, cancellationToken);
                if (!string.IsNullOrEmpty(cachedData))
                {
                    var courtType = JsonSerializer.Deserialize<CourtTypeEntity>(cachedData);
                    _logger.LogInformation($"Court type {courtTypeId} retrieved from cache");
                    return courtType;
                }

                // If not in cache, fetch from database
                var courtTypeEntity = await _repository.GetByIdAsync(courtTypeId);
                Throw.Exception.IfNull(courtTypeEntity, nameof(courtTypeEntity), $"Court type with ID {courtTypeId} not found");

                // Cache the result
                var serialized = JsonSerializer.Serialize(courtTypeEntity);
                await _distributedCache.SetStringAsync(cacheKey, serialized, _cacheOptions, cancellationToken);
                _logger.LogInformation($"Court type {courtTypeId} cached successfully");

                return courtTypeEntity;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetByIdAsync: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Get all court types from cache or database
        /// </summary>
        public async Task<List<CourtTypeEntity>> GetCachedListAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                string cacheKey = CourtTypeCacheKeys.ListKey;

                // Try to get from cache
                var cachedData = await _distributedCache.GetStringAsync(cacheKey, cancellationToken);
                if (!string.IsNullOrEmpty(cachedData))
                {
                    var courtTypeList = JsonSerializer.Deserialize<List<CourtTypeEntity>>(cachedData);
                    _logger.LogInformation($"Court type list retrieved from cache ({courtTypeList?.Count ?? 0} items)");
                    return courtTypeList ?? new List<CourtTypeEntity>();
                }

                // If not in cache, fetch from database
                var courtTypes = await _repository.CourtTypeEntities
                    .AsNoTracking()
                    .OrderBy(x => x.CourtType)
                    .ToListAsync(cancellationToken);

                if (courtTypes.Count > 0)
                {
                    // Cache the result
                    var serialized = JsonSerializer.Serialize(courtTypes);
                    await _distributedCache.SetStringAsync(cacheKey, serialized, _cacheOptions, cancellationToken);
                    _logger.LogInformation($"Court type list cached successfully ({courtTypes.Count} items)");
                }

                return courtTypes;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetCachedListAsync: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Get mapped response DTOs from cache
        /// </summary>
        public async Task<List<GetCourtTypeResponse>> GetCachedMappedListAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var courtTypes = await GetCachedListAsync(cancellationToken);
                var mappedList = _mapper.Map<List<GetCourtTypeResponse>>(courtTypes);
                return mappedList;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetCachedMappedListAsync: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Get court type dropdown data from cache or database
        /// </summary>
        public async Task<List<Dropdown>> GetDropdownAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                string cacheKey = CourtTypeCacheKeys.DropdownKey;

                // Try to get from cache
                var cachedData = await _distributedCache.GetStringAsync(cacheKey, cancellationToken);
                if (!string.IsNullOrEmpty(cachedData))
                {
                    var dropdownList = JsonSerializer.Deserialize<List<Dropdown>>(cachedData);
                    _logger.LogInformation($"Court type dropdown retrieved from cache ({dropdownList?.Count ?? 0} items)");
                    return dropdownList ?? new List<Dropdown>();
                }

                // If not in cache, fetch from database
                var courtTypes = await _repository.CourtTypeEntities
                    .AsNoTracking()
                    .OrderBy(x => x.CourtType)
                    .Select(x => new Dropdown
                    {
                        Id = x.Id,
                        Name = x.CourtType,
                        Code = x.Abbreviation
                    })
                    .ToListAsync(cancellationToken);

                if (courtTypes.Count > 0)
                {
                    // Cache the result
                    var serialized = JsonSerializer.Serialize(courtTypes);
                    await _distributedCache.SetStringAsync(cacheKey, serialized, _cacheOptions, cancellationToken);
                    _logger.LogInformation($"Court type dropdown cached successfully ({courtTypes.Count} items)");
                }

                return courtTypes;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetDropdownAsync: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Invalidate all court type caches
        /// </summary>
        public async Task InvalidateAllCachesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await _distributedCache.RemoveAsync(CourtTypeCacheKeys.ListKey, cancellationToken);
                await _distributedCache.RemoveAsync(CourtTypeCacheKeys.DropdownKey, cancellationToken);
                _logger.LogInformation("All court type caches invalidated");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in InvalidateAllCachesAsync: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Invalidate specific court type cache by ID
        /// </summary>
        public async Task InvalidateCacheByIdAsync(Guid courtTypeId, CancellationToken cancellationToken = default)
        {
            try
            {
                if (courtTypeId == Guid.Empty)
                    throw new ArgumentException("Court type ID cannot be empty.", nameof(courtTypeId));

                string cacheKey = CourtTypeCacheKeys.GetKey(courtTypeId);
                await _distributedCache.RemoveAsync(cacheKey, cancellationToken);

                // Also invalidate list cache since it might contain this item
                await _distributedCache.RemoveAsync(CourtTypeCacheKeys.ListKey, cancellationToken);
                await _distributedCache.RemoveAsync(CourtTypeCacheKeys.DropdownKey, cancellationToken);

                _logger.LogInformation($"Cache invalidated for court type {courtTypeId}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in InvalidateCacheByIdAsync: {ex.Message}");
                throw;
            }
        }
    }
}
