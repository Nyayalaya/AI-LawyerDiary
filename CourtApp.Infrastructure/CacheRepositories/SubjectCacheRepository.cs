using CourtApp.Application.Interfaces.CacheRepositories;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Application.CacheKeys;
using AspNetCoreHero.Extensions.Caching;
using AspNetCoreHero.ThrowR;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using CourtApp.Domain.Entities.Common;

namespace CourtApp.Infrastructure.CacheRepositories
{
    public class SubjectCacheRepository : ISubjectCacheRepository
    {
        private readonly IDistributedCache _distributedCache;
        private readonly ISubjectRepository _repository;

        public SubjectCacheRepository(IDistributedCache distributedCache, ISubjectRepository _repository)
        {
            _distributedCache = distributedCache;
            this._repository = _repository;
        }
        public async Task<SubjectEntity> GetByIdAsync(Guid Id)
        {
            string cacheKey = SubjectCacheKeys.GetKey(Id);
            var subjectlist = await _distributedCache.GetAsync<SubjectEntity>(cacheKey);
            if (subjectlist == null)
            {
                subjectlist = await _repository.GetByIdAsync(Id);
                Throw.Exception.IfNull(subjectlist, "PracticeSubject", "No Subject  Found");
                await _distributedCache.SetAsync(cacheKey, subjectlist);
            }
            return subjectlist;
        }

        public async Task<List<SubjectEntity>> GetCachedListAsync()
        {
            string cacheKey = SubjectCacheKeys.ListKey;
            var subjectlist = await _distributedCache.GetAsync<List<SubjectEntity>>(cacheKey);
            if (subjectlist == null)
            {
                subjectlist = await _repository.GetListAsync();
                await _distributedCache.SetAsync(cacheKey, subjectlist);
            }
            return subjectlist;
        }
    }
}
