using AspNetCoreHero.Extensions.Caching;
using AspNetCoreHero.ThrowR;
using CourtApp.Application.Interfaces.CacheRepositories.FormBuilder;
using CourtApp.Application.Interfaces.Repositories.FormBuilder;
using CourtApp.Domain.Entities.FormBuilder;
using CourtApp.Application.CacheKeys;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.CacheRepositories
{
    public class CaseDraftingCacheRepository : ICaseDraftingCacheRepository
    {
        private readonly ICaseDraftingRepository _repository;
        private readonly IDistributedCache _distributedCache;
        public CaseDraftingCacheRepository(ICaseDraftingRepository _repository, IDistributedCache _distributedCache)
        {
            this._repository = _repository;
            this._distributedCache = _distributedCache;
        }
        public async Task<DraftingDetailEntity> GetByIdAsync(Guid Id)
        {
            string cacheKey = DraftingDetailCacheKeys.GetKey(Id);
            var dt = await _distributedCache.GetAsync<DraftingDetailEntity>(cacheKey);
            if (dt == null)
            {
                dt = await _repository.GetByIdAsync(Id);
                Throw.Exception.IfNull(dt, "Drafting", "Not Found");
                await _distributedCache.SetAsync(cacheKey, dt);
            }
            return dt;
        }

        public async Task<List<DraftingDetailEntity>> GetCachedListAsync()
        {
            string cacheKey = DraftingDetailCacheKeys.ListKey;
            var dt = await _distributedCache.GetAsync<List<DraftingDetailEntity>>(cacheKey);
            if (dt == null)
            {
                dt = await _repository.GetListAsync();
                await _distributedCache.SetAsync(cacheKey, dt);
            }
            return dt;
        }
    }
}
