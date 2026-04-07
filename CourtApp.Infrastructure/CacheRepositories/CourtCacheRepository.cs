using AutoMapper;
using CourtApp.Application.CacheKeys;
using CourtApp.Application.Features.Court.DTOs;
using CourtApp.Application.Interfaces.CacheRepositories;
using CourtApp.Application.Interfaces.Repositories;
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
    public class CourtCacheRepository : ICourtCacheRepository
    {
        private readonly IDistributedCache _distributedCache;
        private readonly ICourtRepository _repository;
        private readonly ILogger<CourtCacheRepository> _logger;
        private readonly DistributedCacheEntryOptions _cacheOptions;
        private readonly IMapper _mapper;

        public CourtCacheRepository(
            IDistributedCache distributedCache,
            ICourtRepository repository,
            ILogger<CourtCacheRepository> logger,
            IMapper mapper)
        {
            _distributedCache = distributedCache;
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24)
            };
        }

        public async Task<CourtResponse> GetAsync(Guid id)
        {
            try
            {
                string cacheKey = CourtCacheKeys.CourtKey(id);

                var cachedData = await _distributedCache.GetStringAsync(cacheKey);
                if (!string.IsNullOrEmpty(cachedData))
                {
                    var courtData = JsonSerializer.Deserialize<CourtResponse>(cachedData);
                    _logger.LogInformation($"Court retrieved from cache");
                    return courtData;
                }

                var court = await _repository.GetByIdAsync(id);
                if (court == null)
                    return null;

                var mappedCourt = _mapper.Map<CourtResponse>(court);
                var serialized = JsonSerializer.Serialize(mappedCourt);
                await _distributedCache.SetStringAsync(cacheKey, serialized, _cacheOptions);
                _logger.LogInformation($"Court cached successfully");

                return mappedCourt;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<List<CourtResponse>> GetByLocationAsync(Guid locationId)
        {
            try
            {
                string cacheKey = CourtCacheKeys.CourtByLocationKey(locationId);

                var cachedData = await _distributedCache.GetStringAsync(cacheKey);
                if (!string.IsNullOrEmpty(cachedData))
                {
                    var courtList = JsonSerializer.Deserialize<List<CourtResponse>>(cachedData);
                    _logger.LogInformation($"Court list retrieved from cache ({courtList?.Count ?? 0} items)");
                    return courtList ?? new List<CourtResponse>();
                }

                var courts = await _repository.GetByLocationIdAsync(locationId, 1, int.MaxValue);
                if (courts.Count == 0)
                    return new List<CourtResponse>();

                var mappedCourts = _mapper.Map<List<CourtResponse>>(courts);
                var serialized = JsonSerializer.Serialize(mappedCourts);
                await _distributedCache.SetStringAsync(cacheKey, serialized, _cacheOptions);
                _logger.LogInformation($"Court list cached successfully ({mappedCourts.Count} items)");

                return mappedCourts;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetByLocationAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            try
            {
                string cacheKey = CourtCacheKeys.CourtKey(id);
                await _distributedCache.RemoveAsync(cacheKey);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in RemoveAsync: {ex.Message}");
                throw;
            }
        }
    }
}
