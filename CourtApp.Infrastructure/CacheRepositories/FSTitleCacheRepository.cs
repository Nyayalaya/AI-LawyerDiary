using AspNetCoreHero.Extensions.Caching;
using AspNetCoreHero.ThrowR;
using CourtApp.Application.Interfaces.CacheRepositories;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using CourtApp.Application.CacheKeys;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.CacheRepositories
{
    public class FSTitleCacheRepository : IFSTitleCacheRepository
    {
        private readonly IDistributedCache _distCache;
        private readonly IFSTitleRepository _Repo;
        public FSTitleCacheRepository(IDistributedCache _distCache, IFSTitleRepository _Repo)
        {
            this._distCache = _distCache;
            this._Repo = _Repo;
        }
        public async Task<FSTitleEntity> GetCachedByIdAsync(Guid Id)
        {
            string cacheKey = FSTitleCacheKeys.GetKey(Id);
            var cachedDt = await _distCache.GetAsync<FSTitleEntity>(cacheKey);
            if (cachedDt == null)
            {
                cachedDt = await _Repo.GetByIdAsync(Id);
                Throw.Exception.IfNull(cachedDt, "FSEntity", "No entity found");
                await _distCache.SetAsync(cacheKey, cachedDt);
            }
            return cachedDt;
        }

        public async Task<List<FSTitleEntity>> GetCachedListAsync()
        {
            string cacheKey = FSTitleCacheKeys.ListKey;
            var dtList = await _distCache.GetAsync<List<FSTitleEntity>>(cacheKey);
            if (dtList == null)
            {
                dtList = await _Repo.GetListAsync();
                await _distCache.SetAsync(cacheKey, dtList);
            }
            return dtList;
        }
    }
}
