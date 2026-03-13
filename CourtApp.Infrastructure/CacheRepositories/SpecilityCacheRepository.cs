using AspNetCoreHero.Extensions.Caching;
using CourtApp.Application.CacheKeys;
using CourtApp.Application.Interfaces.CacheRepositories;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.CacheRepositories
{
    public class SpecilityCacheRepository : ISpecilityCacheRepository
    {
        private readonly IDistributedCache _distributedCache;
        private readonly ISpecilityRepository _repository;
        public SpecilityCacheRepository(IDistributedCache _distributedCache, ISpecilityRepository _repository)
        {
            this._repository = _repository;
            this._distributedCache = _distributedCache;
        }
        public async Task<SpecializationEntity> GetByIdAsync(Guid Id)
        {
            string cacheKey = SpecilityCacheKeys.GetKey(Id);
            var detail = await _distributedCache.GetAsync<SpecializationEntity>(cacheKey);
            if (detail == null)
            {
                detail = await _repository.GetByIdAsync(Id);
                await _distributedCache.SetAsync(cacheKey, detail);
            }
            return detail;
        }

        public async Task<List<SpecializationEntity>> GetCachedListAsync()
        {
            string cacheKey = SpecilityCacheKeys.ListKey;
            var dl = await _distributedCache.GetAsync<List<SpecializationEntity>>(cacheKey);
            if (dl == null)
            {
                dl = await _repository.GetListAsync();
                await _distributedCache.SetAsync(cacheKey, dl);
            }
            return dl;
        }
    }
}
