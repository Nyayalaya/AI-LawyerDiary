using CourtApp.Application.CacheKeys;
using AspNetCoreHero.Extensions.Caching;
using AspNetCoreHero.ThrowR;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Threading.Tasks;
using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Linq;
using CourtApp.Application.Features.CaseCategory.Services;

namespace CourtApp.Infrastructure.CacheRepositories
{
    public class CaseNatureCacheRepository : ICaseCategoryCacheRepository
    {
        private readonly IDistributedCache _distributedCache;
        private readonly ICaseCategoryRepository _natureRepository;
        public CaseNatureCacheRepository(IDistributedCache _distributedCache, ICaseCategoryRepository _natureRepository)
        {
            this._distributedCache = _distributedCache;
            this._natureRepository = _natureRepository;
        }        

        public async Task<NatureEntity> GetByIdAsync(Guid natureId)
        {
            string cacheKey = CaseNatureCacheKeys.GetKey(natureId);
            var bookType = await _distributedCache.GetAsync<NatureEntity>(cacheKey);
            if (bookType == null)
            {
                bookType = await _natureRepository.GetByIdAsync(natureId);
                Throw.Exception.IfNull(bookType, "Brand", "No Brand Found");
                await _distributedCache.SetAsync(cacheKey, bookType);
            }
            return bookType;
        }

        public async Task<List<NatureEntity>> GetCachedListAsync()
        {
            string cacheKey = CaseNatureCacheKeys.ListKey;
            var bookTypeList = await _distributedCache.GetAsync<List<NatureEntity>>(cacheKey);
            if (bookTypeList == null)
            {
                bookTypeList = await _natureRepository.GetListAsync();
                await _distributedCache.SetAsync(cacheKey, bookTypeList);
            }
            return bookTypeList;
        }
    }
}
