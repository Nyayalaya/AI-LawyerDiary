using AspNetCoreHero.Extensions.Caching;
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
    public class CourtComplexCacheRepository : ICourtComplexCacheRepository
    {
        private readonly IDistributedCache _distributedCache;
        private readonly ICourtComplexRepository _repository;
        public CourtComplexCacheRepository(IDistributedCache _distributedCache, 
            ICourtComplexRepository _repository)
        {
            this._repository = _repository;
            this._distributedCache = _distributedCache; 
        }
        public async Task<CourtComplexEntity> GetByIdAsync(Guid Id)
        {
            string cacheKey = CourtComplexCacheKeys.GetByIdKey(Id);
            var detail = await _distributedCache.GetAsync<CourtComplexEntity>(cacheKey);
            if (detail == null)
            {
                detail = await _repository.GetByIdAsync(Id);
                await _distributedCache.SetAsync(cacheKey, detail);
            }
            return detail;
        }

        public async Task<List<CourtComplexEntity>> GetCachedListAsync()
        {
            string cacheKey = CourtComplexCacheKeys.ListKey;
            var dl = await _distributedCache.GetAsync<List<CourtComplexEntity>>(cacheKey);
            if (dl == null)
            {
                dl = await _repository.GetListAsync();
                await _distributedCache.SetAsync(cacheKey, dl);
            }
            return dl;
        }
    }
}
