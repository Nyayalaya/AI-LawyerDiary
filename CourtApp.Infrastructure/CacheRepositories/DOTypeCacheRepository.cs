using AspNetCoreHero.Extensions.Caching;
using AspNetCoreHero.ThrowR;
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
    public class DOTypeCacheRepository : IDOTypeCacheRepository
    {
        private readonly IDistributedCache _distCache;
        private readonly IDOTypeRepository _Repo;
        public DOTypeCacheRepository(IDistributedCache _distCache, IDOTypeRepository _Repo)
        {
            this._distCache = _distCache;
            this._Repo = _Repo; 
        }
        public async Task<DOTypeEntity> GetByIdAsync(Guid Id)
        {
            string cacheKey = DOTypeCacheKeys.GetKey(Id);
            var cachedDt = await _distCache.GetAsync<DOTypeEntity>(cacheKey);
            if (cachedDt == null)
            {
                cachedDt = await _Repo.GetByIdAsync(Id);
                Throw.Exception.IfNull(cachedDt, "DOType", "No Draft and order found");
                await _distCache.SetAsync(cacheKey, cachedDt);
            }
            return cachedDt;
        }

        public async Task<List<DOTypeEntity>> GetCachedListAsync()
        {
            string cacheKey = DOTypeCacheKeys.ListKey;
            var dtList = await _distCache.GetAsync<List<DOTypeEntity>>(cacheKey);
            if (dtList == null)
            {
                dtList = await _Repo.GetListAsync();
                await _distCache.SetAsync(cacheKey, dtList);
            }
            return dtList;
        }
    }
}
