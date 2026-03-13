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
    public class BookTypeCacheRepository : IBookTypeCacheRepository
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IBookTypeRepository _bookTypeRepository;

        public BookTypeCacheRepository(IDistributedCache distributedCache, IBookTypeRepository _bookTypeRepository)
        {
            _distributedCache = distributedCache;
           this. _bookTypeRepository = _bookTypeRepository;
        }

        public async Task<BookTypeEntity> GetByIdAsync(Guid bookTypeId)
        {
            string cacheKey = BookTypeCacheKeys.GetKey(bookTypeId);
            var bookType = await _distributedCache.GetAsync<BookTypeEntity>(cacheKey);
            if (bookType == null)
            {
                bookType = await _bookTypeRepository.GetByIdAsync(bookTypeId);
                Throw.Exception.IfNull(bookType, "BookTypeEntity", "No Book Type Found");
                await _distributedCache.SetAsync(cacheKey, bookType);
            }
            return bookType;
        }

        public async Task<List<BookTypeEntity>> GetCachedListAsync()
        {
            string cacheKey = BookTypeCacheKeys.ListKey;
            var bookTypeList = await _distributedCache.GetAsync<List<BookTypeEntity>>(cacheKey);
            if (bookTypeList==null)
            {
                bookTypeList = await _bookTypeRepository.GetListAsync();
                await _distributedCache.SetAsync(cacheKey, bookTypeList);
            }
            return bookTypeList;
        }       
    }
}