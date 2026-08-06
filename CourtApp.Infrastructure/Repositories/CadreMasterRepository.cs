using CourtApp.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourtApp.Application.CacheKeys;
using CourtApp.Domain.Entities.Masters;
using CourtApp.Application.Features.Cadre.Services;
namespace CourtApp.Infrastructure.Repositories
{
    public class CadreMasterRepository : ICadreMasterRepository
    {
        private readonly IRepositoryAsync<CadreEntity> _repository;
        private readonly IDistributedCache _distributedCache;

        public CadreMasterRepository(IRepositoryAsync<CadreEntity> _repository,
            IDistributedCache _distributedCache)
        {
            this._distributedCache = _distributedCache;
            this._repository = _repository;
        }
        public IQueryable<CadreEntity> Entities => _repository.Entities;

        public async Task DeleteAsync(CadreEntity entity)
        {
            await _repository.DeleteAsync(entity);
            await _distributedCache.RemoveAsync(CadreMasterCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CadreMasterCacheKeys.GetKey(entity.Id));
        }

        public async Task<CadreEntity> GetByIdAsync(Guid id)
        {
            return await _repository
                .Entities
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<CadreEntity>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<Guid> InsertAsync(CadreEntity entity)
        {
            await _repository.AddAsync(entity);
            await _distributedCache.RemoveAsync(CadreMasterCacheKeys.ListKey);
            return entity.Id;
        }

        public async Task UpdateAsync(CadreEntity entity)
        {
            await _repository.UpdateAsync(entity);
            await _distributedCache.RemoveAsync(CadreMasterCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CadreMasterCacheKeys.GetKey(entity.Id));
        }
    }
}
