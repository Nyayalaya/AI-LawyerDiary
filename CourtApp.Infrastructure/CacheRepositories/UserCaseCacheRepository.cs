using AspNetCoreHero.Extensions.Caching;
using AspNetCoreHero.ThrowR;
using CourtApp.Application.Interfaces.CacheRepositories;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.CaseDetails;
using CourtApp.Application.CacheKeys;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.CacheRepositories
{
    internal class UserCaseCacheRepository : IUserCaseCacheRepository
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IUserCaseRepository _repository;
        public UserCaseCacheRepository(IUserCaseRepository _repository, IDistributedCache _distributedCache)
        {
            this._repository = _repository;
            this._distributedCache = _distributedCache;
        }
        public async Task<CaseDetailEntity> GetByIdAsync(Guid CaseUid)
        {
            string cacheKey = UserCaseCacheKeys.GetKey(CaseUid);
            var detail = await _distributedCache.GetAsync<CaseDetailEntity>(cacheKey);
            if (detail == null)
            {
                detail = await _repository.GetByIdAsync(CaseUid);
                Throw.Exception.IfNull(detail, "User Case Detail", "No Case Found");
                await _distributedCache.SetAsync(cacheKey, detail);
            }
            return detail;
        }

        public Task<List<CaseDetailEntity>> GetCachedListAsync()
        {
            throw new NotImplementedException();
        }
    }
}
