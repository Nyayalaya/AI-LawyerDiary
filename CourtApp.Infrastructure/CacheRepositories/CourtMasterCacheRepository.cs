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
    public class CourtMasterCacheRepository : ICourtMasterCacheRepository
    {
        private readonly IDistributedCache _distributedCache;
        private readonly ICourtMasterRepository _repository;
        public CourtMasterCacheRepository(ICourtMasterRepository _repository, IDistributedCache _distributedCache)
        {
            this._repository = _repository;
            this._distributedCache = _distributedCache;
        }
        public async Task<CourtMasterEntity> GetByIdAsync(Guid UniqueId)
        {
            string cacheKey = AppCacheKeys.CourtMasterKey;
            var bookType = await _distributedCache.GetAsync<CourtMasterEntity>(cacheKey);
            if (bookType == null)
            {
                bookType = await _repository.GetByIdAsync(UniqueId);
                Throw.Exception.IfNull(bookType, "CourtEntity", "No Court Found");
                await _distributedCache.SetAsync(cacheKey, bookType);
            }
            return bookType;
        }

        public async Task<List<CourtMasterEntity>> GetCachedListAsync()
        {
            string cacheKey = AppCacheKeys.CourtMasterKey;
            var bookTypeList = await _distributedCache.GetAsync<List<CourtMasterEntity>>(cacheKey);
            if (bookTypeList == null)
            {
                bookTypeList = await _repository.GetListAsync();
                await _distributedCache.SetAsync(cacheKey, bookTypeList);
            }
            return bookTypeList;
        }
    }
}
