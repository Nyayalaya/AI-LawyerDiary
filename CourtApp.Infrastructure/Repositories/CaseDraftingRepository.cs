using CourtApp.Application.CacheKeys;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Application.Interfaces.Repositories.FormBuilder;
using CourtApp.Domain.Entities.FormBuilder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Repositories
{
    public class CaseDraftingRepository : ICaseDraftingRepository
    {
        private readonly IRepositoryAsync<DraftingDetailEntity> _repository;
        private readonly IDistributedCache _distributedCache;
        public CaseDraftingRepository(IRepositoryAsync<DraftingDetailEntity> _repository,
            IDistributedCache _distributedCache)
        {
            this._repository = _repository;
            this._distributedCache = _distributedCache;
        }
        public IQueryable<DraftingDetailEntity> Entities => _repository.Entities;

        public async Task DeleteAsync(DraftingDetailEntity entity)
        {
            await _repository.DeleteAsync(entity);
            await _distributedCache.RemoveAsync(DraftingDetailCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(DraftingDetailCacheKeys.GetKey(entity.Id));
        }

        public async Task<DraftingDetailEntity> GetByIdAsync(Guid Id)
        {
            return await _repository.Entities.Where(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<DraftingDetailEntity>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<Guid> InsertAsync(DraftingDetailEntity entity)
        {
            await _repository.AddAsync(entity);
            await _distributedCache.RemoveAsync(DraftingDetailCacheKeys.ListKey);
            return entity.Id;
        }

        public async Task UpdateAsync(DraftingDetailEntity entity)
        {
            await _repository.UpdateAsync(entity);
            await _distributedCache.RemoveAsync(DraftingDetailCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(DraftingDetailCacheKeys.GetKey(entity.Id));
        }
    }
}
