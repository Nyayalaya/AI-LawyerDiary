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
    public class LawyerCacheRepository : ILawyerCacheRepository
    {
        private readonly IDistributedCache _distCache;
        private readonly ILawyerRepository _Repo;
        public LawyerCacheRepository(IDistributedCache _distCache, ILawyerRepository _Repo)
        {
            this._distCache = _distCache;
            this._Repo = _Repo;
        }
        public async Task<LawyerMasterEntity> GetByIdAsync(Guid Id)
        {
            string cacheKey = LawyerMasterCacheKeys.GetKey(Id);
            var cachedDt = await _distCache.GetAsync<LawyerMasterEntity>(cacheKey);
            if (cachedDt == null)
            {
                cachedDt = await _Repo.GetByIdAsync(Id);
                Throw.Exception.IfNull(cachedDt, "Lawyer", "No lawyer found");
                await _distCache.SetAsync(cacheKey, cachedDt);
            }
            return cachedDt;
        }

        public Task<List<LawyerMasterEntity>> GetCachedLawyerDt()
        {
            throw new NotImplementedException();
        }

        public async Task<List<LawyerMasterEntity>> GetCachedListAsync()
        {
            string cacheKey = LawyerMasterCacheKeys.ListKey;
            var dtList = await _distCache.GetAsync<List<LawyerMasterEntity>>(cacheKey);
            if (dtList==null)
            {
                dtList = await _Repo.GetListAsync();
                await _distCache.SetAsync(cacheKey, dtList);
            }
            return dtList;
        }
    }
}
