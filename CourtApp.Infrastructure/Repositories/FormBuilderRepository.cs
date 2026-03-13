using CourtApp.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using CourtApp.Application.Interfaces.Repositories.FormBuilder;
using CourtApp.Domain.Entities.FormBuilder;
using CourtApp.Application.CacheKeys;

namespace CourtApp.Infrastructure.Repositories
{
    public class FormBuilderRepository : IFormBuilderRepository
    {
        private readonly IRepositoryAsync<FormBuilderEntity> _repository;
        private readonly IDistributedCache _distributedCache;
        public FormBuilderRepository(IRepositoryAsync<FormBuilderEntity> _repository,
            IDistributedCache _distributedCache)
        {
            this._repository = _repository;
            this._distributedCache = _distributedCache;
        }
        public IQueryable<FormBuilderEntity> Entities => _repository.Entities;

        public async Task DeleteAsync(FormBuilderEntity entity)
        {
            await _repository.DeleteAsync(entity);
            await _distributedCache.RemoveAsync(FormBuilderCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(FormBuilderCacheKeys.GetKey(entity.Id));
        }

        public async Task<FormBuilderEntity> GetByIdAsync(Guid Id)
        {
            return await _repository.Entities.Where(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<FormBuilderEntity>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<Guid> InsertAsync(FormBuilderEntity entity)
        {
            await _repository.AddAsync(entity);
            await _distributedCache.RemoveAsync(FormBuilderCacheKeys.ListKey);
            return entity.Id;
        }

        public async Task UpdateAsync(FormBuilderEntity entity)
        {
            await _repository.UpdateAsync(entity);
            await _distributedCache.RemoveAsync(FormBuilderCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(FormBuilderCacheKeys.GetKey(entity.Id));
        }
    }
}
