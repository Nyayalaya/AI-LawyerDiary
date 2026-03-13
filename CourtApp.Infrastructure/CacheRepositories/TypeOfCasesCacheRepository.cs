using CourtApp.Application.CacheKeys;
using AspNetCoreHero.Extensions.Caching;
using AspNetCoreHero.ThrowR;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Threading.Tasks;
using CourtApp.Domain.Entities.LawyerDiary;
using System;
using CourtApp.Application.Features.CaseType.Services;

namespace CourtApp.Infrastructure.CacheRepositories
{
    public class TypeOfCasesCacheRepository: ICaseTypeCacheRepository
    {
        private readonly ICaseTypeRepository _repository;
        private readonly IDistributedCache _distributedCache;

        public TypeOfCasesCacheRepository(ICaseTypeRepository _repository, IDistributedCache _distributedCache)
        {
            this._distributedCache = _distributedCache;
            this._repository = _repository;
        }

        public async Task<TypeOfCasesEntity> GetByIdAsync(Guid Id)
        {
            string cacheKey = TypeOfCasesCacheKeys.GetKey(Id);
            var cachedata = await  _distributedCache.GetAsync<TypeOfCasesEntity>(cacheKey);
            if (cachedata == null)
            {
                cachedata = await _repository.GetByIdAsync(Id);
                Throw.Exception.IfNull(cachedata, "Typeofcasess", "No Type of cases Found");
                await _distributedCache.SetAsync(cacheKey, cachedata);
            }
            return cachedata;
        }

        public async Task<List<TypeOfCasesEntity>> GetCachedListAsync()
        {
            string cacheKey = TypeOfCasesCacheKeys.ListKey;
            var cachedataList = await _distributedCache.GetAsync<List<TypeOfCasesEntity>>(cacheKey);
            if (cachedataList == null)
            {
                cachedataList = await _repository.GetListAsync();
                await _distributedCache.SetAsync(cacheKey, cachedataList);
            }
            return cachedataList;
        }
    }
}
