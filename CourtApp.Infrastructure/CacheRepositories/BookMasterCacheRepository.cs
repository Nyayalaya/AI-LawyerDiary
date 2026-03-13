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
    public class BookMasterCacheRepository : IBookMasterCacheRepository
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IBookMasterRepository _Repository;

        public BookMasterCacheRepository(IDistributedCache distributedCache, IBookMasterRepository _Repository)
        {
            _distributedCache = distributedCache;
            this._Repository = _Repository;
        }       

        public async Task<LDBookEntity> GetByIdAsync(Guid bookId)
        {
            string cacheKey = BookMasterCacheKeys.GetKey(bookId);
            var brand = await _distributedCache.GetAsync<LDBookEntity>(cacheKey);
            if (brand == null)
            {
                brand = await _Repository.GetByIdAsync(bookId);
                Throw.Exception.IfNull(brand, "BookMaster", "No Book Master Found");
                await _distributedCache.SetAsync(cacheKey, brand);
            }
            return brand;
        }

        public async Task<List<LDBookEntity>> GetCachedListAsync()
        {
            string cacheKey = BookMasterCacheKeys.ListKey;
            var bookList = await _distributedCache.GetAsync<List<LDBookEntity>>(cacheKey);
            if (bookList == null)
            {
                bookList = await _Repository.GetListAsync();
                await _distributedCache.SetAsync(cacheKey, bookList);
            }
            return bookList;
        }

       
    }
}