using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourtApp.Application.CacheKeys;
using CourtApp.Application.Features.CaseCategory.Services;

namespace CourtApp.Infrastructure.Repositories
{
    public class CaseNatureRepository : ICaseCategoryRepository
    {
        private readonly IRepositoryAsync<NatureEntity> _repository;
        private readonly IDistributedCache _distributedCache;
        public CaseNatureRepository(IRepositoryAsync<NatureEntity> _repository, IDistributedCache _distributedCache)
        {
            this._distributedCache = _distributedCache;
            this._repository = _repository;
        }
        public IQueryable<NatureEntity> CaseNatures => _repository.Entities;

        public async Task DeleteAsync(NatureEntity caseNature)
        {
            await _repository.DeleteAsync(caseNature);
            await _distributedCache.RemoveAsync(CaseNatureCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CaseNatureCacheKeys.GetKey(caseNature.Id));
        }

        public async Task<NatureEntity> GetByIdAsync(Guid caseNatureId)
        {
            return await _repository.Entities.Where(c => c.Id == caseNatureId).FirstOrDefaultAsync();
        }

        public async Task<List<NatureEntity>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<Guid> InsertAsync(NatureEntity caseNature)
        {
            await _repository.AddAsync(caseNature);
            await _distributedCache.RemoveAsync(CaseNatureCacheKeys.ListKey);
            return caseNature.Id;
        }

        public async Task<bool> IsCaseCategoryExistAsync(Guid courtTypeId, string category)
        {
            var caseCategory = await _repository
                .Entities
                .AnyAsync(c => c.CourtTypeId == courtTypeId && c.Name_En == category);

            return caseCategory;
        }

        public async Task UpdateAsync(NatureEntity caseNature)
        {
            await _repository.UpdateAsync(caseNature);
            await _distributedCache.RemoveAsync(CaseNatureCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CaseNatureCacheKeys.GetKey(caseNature.Id));
        }
    }
}
