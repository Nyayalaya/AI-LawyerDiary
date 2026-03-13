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
    public class PublisherCacheRepository : IPublicationCacheRepository
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IPublicationRepository _Repository;

        public PublisherCacheRepository(IDistributedCache distributedCache, IPublicationRepository _Repository)
        {
            _distributedCache = distributedCache;
            this._Repository = _Repository;
        }
        public async Task<PublisherEntity> GetByIdAsync(Guid Id)
        {
            string cacheKey = PublisherCacheKeys.GetKey(Id);
            var List = await _distributedCache.GetAsync<PublisherEntity>(cacheKey);
            if (List == null)
            {
                List = await _Repository.GetByIdAsync(Id);
                Throw.Exception.IfNull(List, "PublisherEntity", "No Publication Found");
                await _distributedCache.SetAsync(cacheKey, List);
            }
            return List;
        }

        public async Task<List<PublisherEntity>> GetCachedListAsync()
        {
            string cacheKey = PublisherCacheKeys.ListKey;
            var List = await _distributedCache.GetAsync<List<PublisherEntity>>(cacheKey);
            if (List == null)
            {
                List = await _Repository.GetListAsync();
                await _distributedCache.SetAsync(cacheKey, List);
            }
            return List;
        }
    }
}
