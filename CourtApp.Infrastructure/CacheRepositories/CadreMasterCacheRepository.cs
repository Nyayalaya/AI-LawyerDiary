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
    public class CadreMasterCacheRepository : ICadreMasterCacheRepository
    {
        private readonly IDistributedCache _distributedCache;
        private readonly ICadreMasterRepository _Repository;
        public CadreMasterCacheRepository(IDistributedCache distributedCache,
            ICadreMasterRepository _Repository)
        {
            _distributedCache = distributedCache;
            this._Repository = _Repository;
        }
        public async Task<CadreMasterEntity> GetByIdAsync(Guid id)
        {
            string cacheKey = CadreMasterCacheKeys.GetKey(id);
            var detail = await _distributedCache.GetAsync<CadreMasterEntity>(cacheKey);
            if (detail == null)
            {
                detail = await _Repository.GetByIdAsync(id);
                Throw.Exception.IfNull(detail, "CadreMaster", "No Cadre record found");
                await _distributedCache.SetAsync(cacheKey, detail);
            }
            return detail;
        }

        public async Task<List<CadreMasterEntity>> GetCachedListAsync()
        {
            string cacheKey = CadreMasterCacheKeys.ListKey;
            var CadreDataList = await _distributedCache.GetAsync<List<CadreMasterEntity>>(cacheKey);
            if (CadreDataList == null)
            {
                CadreDataList = await _Repository.GetListAsync();
                await _distributedCache.SetAsync(cacheKey, CadreDataList);
            }
            return CadreDataList;
        }
    }
}
