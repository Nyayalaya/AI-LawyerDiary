using CourtApp.Application.CacheKeys;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.Masters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Repositories
{
    public class FormTypeRepository : IFormTypeRepository
    {
        private readonly IRepositoryAsync<FormTypeEntity> _repository;
        private readonly IDistributedCache _distributedCache;

        public FormTypeRepository(IRepositoryAsync<FormTypeEntity> repository, IDistributedCache distributedCache)
        {
            _repository = repository;
            _distributedCache = distributedCache;
        }

        public IQueryable<FormTypeEntity> Entities => _repository.Entities;

        public async Task<FormTypeEntity> GetByIdAsync(Guid id)
        {
            var formType = await _repository.Entities
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
            return formType;
        }

        public async Task<FormTypeEntity> GetByCodeAsync(string code)
        {
            var formType = await _repository.Entities
                .Where(p => p.Code == code)
                .FirstOrDefaultAsync();
            return formType;
        }

        public async Task<List<FormTypeEntity>> GetListAsync()
        {
            return await _repository.Entities
                .OrderBy(o => o.Name)
                .ToListAsync();
        }

        public async Task<Guid> InsertAsync(FormTypeEntity entity)
        {
            await _repository.AddAsync(entity);
            await InvalidateCacheAsync();
            return entity.Id;
        }

        public async Task UpdateAsync(FormTypeEntity entity)
        {
            await _repository.UpdateAsync(entity);
            await InvalidateCacheAsync();
        }

        public async Task DeleteAsync(FormTypeEntity entity)
        {
            await _repository.DeleteAsync(entity);
            await InvalidateCacheAsync();
        }

        private async Task InvalidateCacheAsync()
        {
            await _distributedCache.RemoveAsync(AppCacheKeys.FormTypeListKey);
        }
    }
}
