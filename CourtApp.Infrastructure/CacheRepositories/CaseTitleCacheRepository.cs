using AspNetCoreHero.Extensions.Caching;
using AspNetCoreHero.ThrowR;
using CourtApp.Application.Interfaces.CacheRepositories;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.CaseDetails;
using CourtApp.Application.CacheKeys;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.CacheRepositories
{
    public class CaseTitleCacheRepository : ICaseTitleCacheRepository
    {
        private readonly IDistributedCache _distributedCache;
        private readonly ICaseTitleRepository _Repository;
        public CaseTitleCacheRepository(IDistributedCache _distributedCache, ICaseTitleRepository _repository)
        {
            this._distributedCache = _distributedCache;
            this._Repository = _repository;
        }        
        public async Task<CaseTitleEntity> GetByIdAsync(Guid CaseId)
        {
            string cacheKey = CaseTitleCacheKeys.GetKey(CaseId);
            var dt = await _distributedCache.GetAsync<CaseTitleEntity>(cacheKey);
            if (dt == null)
            {
                dt = await _Repository.GetByIdAsync(CaseId);
                Throw.Exception.IfNull(dt, "CaseTitle", "No title found agains case");
                await _distributedCache.SetAsync(cacheKey, dt);
            }
            return dt;
        }

        public async Task<List<CaseTitleEntity>> GetCachedListAsync()
        {
            string cacheKey = CaseTitleCacheKeys.ListKey;
            var dataList = await _distributedCache.GetAsync<List<CaseTitleEntity>>(cacheKey);
            if (dataList == null)
            {
                dataList = await _Repository.GetListAsync();
                await _distributedCache.SetAsync(cacheKey, dataList);
            }
            return dataList;
        }
    }
}
