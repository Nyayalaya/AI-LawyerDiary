using AspNetCoreHero.Extensions.Caching;
using AspNetCoreHero.ThrowR;
using AutoMapper;
using CourtApp.Application.CacheKeys;
using CourtApp.Application.Common;
using CourtApp.Application.Constants;
using CourtApp.Application.Features.CaseStage.Query;
using CourtApp.Application.Features.CaseStage.Services;
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
    public class CaseStageCacheRepository : ICaseStageCacheRepository
    {
        private readonly IDistributedCache _distributedCache;
        private readonly ICaseStageRepository _repository;
        private readonly ILogger<CaseStageCacheRepository> logger;
        private readonly DistributedCacheEntryOptions _cacheOptions;
        private readonly IMapper mapper;
        public CaseStageCacheRepository(
            ICaseStageRepository _repository, 
            IDistributedCache _distributedCache, 
            ILogger<CaseStageCacheRepository> _logger, 
            IMapper _mapper)
        {
            this._repository = _repository;
            this._distributedCache = _distributedCache;
            logger = _logger;
            mapper = _mapper;
            _cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(25)
            };

        }
        public async Task<CaseStageEntity> GetByIdAsync(Guid Id)
        {
            string cacheKey = CaseStageCacheKeys.GetKey(Id);
            var bookType = await _distributedCache.GetAsync<CaseStageEntity>(cacheKey);
            if (bookType == null)
            {
                bookType = await _repository.GetByIdAsync(Id);
                Throw.Exception.IfNull(bookType, "CaseStage", "No Case Stage Found");
                await _distributedCache.SetAsync(cacheKey, bookType);
            }
            return bookType;
        }

        public async Task<List<CaseStageEntity>> GetCachedListAsync()
        {
            string cacheKey = CaseStageCacheKeys.ListKey;
            var bookTypeList = await _distributedCache.GetAsync<List<CaseStageEntity>>(cacheKey);
            if (bookTypeList == null)
            {
                bookTypeList = await _repository.GetListAsync();
                await _distributedCache.SetAsync(cacheKey, bookTypeList);
            }
            return bookTypeList;
        }

        public async Task<List<CaseStageEntity>> GetCachedListAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                string cacheKey = CacheKeys.List<CaseStageEntity>();

                // Try to get from cache
                var cachedData = await _distributedCache.GetStringAsync(cacheKey, cancellationToken);
                if (!string.IsNullOrEmpty(cachedData))
                {
                    var data = JsonSerializer.Deserialize<List<CaseStageEntity>>(cachedData);
                    logger.LogInformation($"Case stage retrieved from cache ({data?.Count ?? 0} items)");
                    return data ?? new List<CaseStageEntity>();
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
                    logger.LogInformation($"Case stage list cached successfully ({courtTypes.Count} items)");
                }

                return courtTypes;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error in GetCachedListAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<List<CaseStageResponse>> GetCachedMappedListAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var states = await GetCachedListAsync(cancellationToken);
                var mappedList = mapper.Map<List<CaseStageResponse>>(states);
                return mappedList;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error in GetCachedMappedListAsync: {ex.Message}");
                throw;
            }
        }

        public Task<List<Dropdown>> GetDropdownAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
