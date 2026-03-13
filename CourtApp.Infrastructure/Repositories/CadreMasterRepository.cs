using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourtApp.Application.CacheKeys;
namespace CourtApp.Infrastructure.Repositories
{
    public class CadreMasterRepository : ICadreMasterRepository
    {
        private readonly IRepositoryAsync<CadreMasterEntity> _repository;
        private readonly IDistributedCache _distributedCache;

        public CadreMasterRepository(IRepositoryAsync<CadreMasterEntity> _repository,
            IDistributedCache _distributedCache)
        {
            this._distributedCache = _distributedCache;
            this._repository = _repository;
        }
        public IQueryable<CadreMasterEntity> Entities => _repository.Entities;

        public async Task DeleteAsync(CadreMasterEntity entity)
        {
            await _repository.DeleteAsync(entity);
            await _distributedCache.RemoveAsync(CadreMasterCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CadreMasterCacheKeys.GetKey(entity.Id));
        }

        public async Task<CadreMasterEntity> GetByIdAsync(Guid id)
        {
            return await _repository
                .Entities
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<CadreMasterEntity>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<Guid> InsertAsync(CadreMasterEntity entity)
        {
            await _repository.AddAsync(entity);
            await _distributedCache.RemoveAsync(CadreMasterCacheKeys.ListKey);
            return entity.Id;
        }

        public async Task UpdateAsync(CadreMasterEntity entity)
        {
            await _repository.UpdateAsync(entity);
            await _distributedCache.RemoveAsync(CadreMasterCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CadreMasterCacheKeys.GetKey(entity.Id));
        }
    }
}
