using CourtApp.Application.Interfaces.CacheRepositories;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Application.CacheKeys;
using AspNetCoreHero.Extensions.Caching;
using AspNetCoreHero.ThrowR;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Threading.Tasks;
using CourtApp.Domain.Entities.LawyerDiary;
using System.Linq;
using System;

namespace CourtApp.Infrastructure.CacheRepositories
{
    public class CaseKindCacheRepository : ICaseKindCacheRepository
    {
        private readonly ICaseKindRepository _repository;
        private readonly IDistributedCache _distributedCache;

        public CaseKindCacheRepository(ICaseKindRepository _repository, IDistributedCache _distributedCache)
        {
            this._distributedCache = _distributedCache;
            this._repository = _repository;
        }

        

        public async Task<CaseKindEntity> GetByIdAsync(Guid Id)
        {
            string cacheKey = CaseKindCacheKeys.GetKey(Id);
            var cachedata = await  _distributedCache.GetAsync<CaseKindEntity>(cacheKey);
            if (cachedata == null)
            {
                cachedata = await _repository.GetByIdAsync(Id);
                Throw.Exception.IfNull(cachedata, "CaseKind", "No CaseKind Found");
                await _distributedCache.SetAsync(cacheKey, cachedata);
            }
            return cachedata;
        }

        public async Task<List<CaseKindEntity>> GetCachedListAsync()
        {
            string cacheKey = CaseKindCacheKeys.ListKey;
            var cachedataList = await _distributedCache.GetAsync<List<CaseKindEntity>>(cacheKey);
            if (cachedataList == null)
            {
                cachedataList = await _repository.GetListAsync();
                await _distributedCache.SetAsync(cacheKey, cachedataList);
            }
            return cachedataList;
        }
    }
}
