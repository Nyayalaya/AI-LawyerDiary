using CourtApp.Application.Interfaces.CacheRepositories;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Application.CacheKeys;
using AspNetCoreHero.Extensions.Caching;
using AspNetCoreHero.ThrowR;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Threading.Tasks;
using CourtApp.Domain.Entities.LawyerDiary;
using System;


namespace CourtApp.Infrastructure.CacheRepositories
{
    public class CaseStageCacheRepository : ICaseStageCacheRepository
    {
        private readonly IDistributedCache _distributedCache;
        private readonly ICaseStageRepository _repository;
        public CaseStageCacheRepository(ICaseStageRepository _repository, IDistributedCache _distributedCache)
        {
            this._repository = _repository;
            this._distributedCache = _distributedCache;
        }
        public async Task<CaseStageEntity> GetByIdAsync(Guid Id)
        {
            string cacheKey = CaseStageCacheKeys.GetKey(Id);
            var bookType = await _distributedCache.GetAsync<CaseStageEntity>(cacheKey);
            if (bookType == null)
            {
                bookType = await _repository.GetByIdAsync(Id);
                Throw.Exception.IfNull(bookType, "CaseStage", "No Case Stage Found");
                await _distributedCache.SetAsync(cacheKey, bookType);
            }
            return bookType;
        }

        public async Task<List<CaseStageEntity>> GetCachedListAsync()
        {
            string cacheKey = CaseStageCacheKeys.ListKey;
            var bookTypeList = await _distributedCache.GetAsync<List<CaseStageEntity>>(cacheKey);
            if (bookTypeList == null)
            {
                bookTypeList = await _repository.GetListAsync();
                await _distributedCache.SetAsync(cacheKey, bookTypeList);
            }
            return bookTypeList;
        }
    }
}
