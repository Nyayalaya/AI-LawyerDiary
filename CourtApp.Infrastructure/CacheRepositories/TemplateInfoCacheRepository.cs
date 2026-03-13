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
    public class TemplateInfoCacheRepository : ITemplateInfoCacheRepository
    {
        private readonly ITemplateInfoRepository _repository;
        private readonly IDistributedCache _distributedCache;
        public TemplateInfoCacheRepository(ITemplateInfoRepository _repository,
           IDistributedCache _distributedCache)
        {
            this._repository = _repository;
            this._distributedCache = _distributedCache;
        }
        public async Task<TemplateInfoEntity> GetByIdAsync(Guid Id)
        {
            string cacheKey = TemplateInfoCacheKey.GetKey(Id);
            var dt = await _distributedCache.GetAsync<TemplateInfoEntity>(cacheKey);
            if (dt == null)
            {
                dt = await _repository.GetByIdAsync(Id);
                Throw.Exception.IfNull(dt, "TemplateInfo", "No Template Found");
                await _distributedCache.SetAsync(cacheKey, dt);
            }
            return dt;
        }

        public async Task<List<TemplateInfoEntity>> GetCachedListAsync()
        {
            string cacheKey = TemplateInfoCacheKey.ListKey;
            var dtl = await _distributedCache.GetAsync<List<TemplateInfoEntity>>(cacheKey);
            if (dtl == null)
            {
                dtl = await _repository.GetListAsync();
                await _distributedCache.SetAsync(cacheKey, dtl);
            }
            return dtl;
        }
    }
}
