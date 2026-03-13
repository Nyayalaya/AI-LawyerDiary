using CourtApp.Application.CacheKeys;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Repositories
{
    public class DOTypeRepository : IDOTypeRepository
    {
        private readonly IRepositoryAsync<DOTypeEntity> _repository;
        private readonly IDistributedCache _distributedCache;
        public DOTypeRepository(IRepositoryAsync<DOTypeEntity> _repository, IDistributedCache _distributedCache)
        {
            this._repository = _repository;
            this._distributedCache = _distributedCache;
        }
        public IQueryable<DOTypeEntity> Entities => _repository.Entities;

        public async Task DeleteAsync(DOTypeEntity entity)
        {
            await _repository.DeleteAsync(entity);
            await _distributedCache.RemoveAsync(DOTypeCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(DOTypeCacheKeys.GetKey(entity.Id));
        }

        public async Task<DOTypeEntity> GetByIdAsync(Guid Id)
        {
            var DetailById = _repository.Entities
                .Where(p => p.Id == Id).FirstOrDefaultAsync();
            return await DetailById;
        }

        public Task<List<DOTypeEntity>> GetListAsync()
        {
            return _repository.Entities.ToListAsync();
        }

        public async Task<Guid> InsertAsync(DOTypeEntity entity)
        {
            await _repository.AddAsync(entity);
            await _distributedCache.RemoveAsync(DOTypeCacheKeys.ListKey);
            return entity.Id;
        }

        public async Task UpdateAsync(DOTypeEntity entity)
        {
            await _repository.UpdateAsync(entity);
            await _distributedCache.RemoveAsync(DOTypeCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(DOTypeCacheKeys.GetKey(entity.Id));
        }
    }
}
