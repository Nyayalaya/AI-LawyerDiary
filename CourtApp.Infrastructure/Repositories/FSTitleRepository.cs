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
    public class FSTitleRepository : IFSTitleRepository
    {
        private readonly IRepositoryAsync<FSTitleEntity> _repository;
        private readonly IDistributedCache _distributedCache;
        public FSTitleRepository(IRepositoryAsync<FSTitleEntity> _repository, IDistributedCache _distributedCache)
        {
            this._repository = _repository;
            this._distributedCache = _distributedCache;
        }
        public IQueryable<FSTitleEntity> Entities => _repository.Entities;

        public async Task DeleteAsync(FSTitleEntity entity)
        {
            await _repository.DeleteAsync(entity);
            await _distributedCache.RemoveAsync(DOTypeCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(FSTitleCacheKeys.GetKey(entity.Id));
        }

        public async Task<FSTitleEntity> GetByIdAsync(Guid Id)
        {
            var DetailById = _repository.Entities
               .Where(p => p.Id == Id).FirstOrDefaultAsync();
            return await DetailById;
        }

        public Task<List<FSTitleEntity>> GetListAsync()
        {
            return _repository.Entities.ToListAsync();
        }

        public async Task<Guid> InsertAsync(FSTitleEntity entity)
        {
            await _repository.AddAsync(entity);
            await _distributedCache.RemoveAsync(FSTitleCacheKeys.ListKey);
            return entity.Id;
        }

        public async Task UpdateAsync(FSTitleEntity entity)
        {
            await _repository.UpdateAsync(entity);
            await _distributedCache.RemoveAsync(FSTitleCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(FSTitleCacheKeys.GetKey(entity.Id));
        }
    }
}
