using AspNetCoreHero.Extensions.Caching;
using CourtApp.Application.Interfaces.CacheRepositories;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using CourtApp.Application.CacheKeys;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.CacheRepositories
{
    public class CourtDistrictCacheRepository : ICourtDistrictCacheRepository
    {
        private readonly IDistributedCache _distributedCache;
        private readonly ICourtDistrictRepository _repository;
        public CourtDistrictCacheRepository(IDistributedCache _distributedCache,
            ICourtDistrictRepository _repository)
        {
            this._repository = _repository;
            this._distributedCache = _distributedCache;
        }
        public async Task<CourtDistrictEntity> GetByIdAsync(Guid Id)
        {
            string cacheKey = CourtDistrictCacheKeys.GetByIdKey(Id);
            var detail = await _distributedCache.GetAsync<CourtDistrictEntity>(cacheKey);
            if (detail == null)
            {
                detail = await _repository.GetByIdAsync(Id);
                await _distributedCache.SetAsync(cacheKey, detail);
            }
            return detail;
        }

        public async Task<List<CourtDistrictEntity>> GetCachedListAsync()
        {
            string cacheKey = CourtDistrictCacheKeys.ListKey;
            var dl = await _distributedCache.GetAsync<List<CourtDistrictEntity>>(cacheKey);
            if (dl == null)
            {
                dl = await _repository.GetListAsync();
                await _distributedCache.SetAsync(cacheKey, dl);
            }
            return dl;
        }
    }
}
