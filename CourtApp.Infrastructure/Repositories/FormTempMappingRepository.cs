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
    public class FormTempMappingRepository : IFormTempMappingRepository
    {
        private readonly IRepositoryAsync<FormTemplateMappingEntity> _repository;
        private readonly IDistributedCache _distributedCache;
        public FormTempMappingRepository(IRepositoryAsync<FormTemplateMappingEntity> _repository, IDistributedCache _distributedCache)
        {
            this._distributedCache = _distributedCache;
            this._repository = _repository;
        }
        public IQueryable<FormTemplateMappingEntity> Entities => _repository.Entities;

        public async Task DeleteAsync(FormTemplateMappingEntity entity)
        {
            await _repository.DeleteAsync(entity);
            await _distributedCache.RemoveAsync(FormTempMappingCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(FormTempMappingCacheKeys.GetKey(entity.Id));
        }

        public async Task<FormTemplateMappingEntity> GetByIdAsync(Guid Id)
        {
            return await _repository
                .Entities
                .Where(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<FormTemplateMappingEntity>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<Guid> InsertAsync(FormTemplateMappingEntity entity)
        {
            await _repository.AddAsync(entity);
            await _distributedCache.RemoveAsync(FormTempMappingCacheKeys.ListKey);
            return entity.Id;
        }

        public async Task UpdateAsync(FormTemplateMappingEntity entity)
        {
            await _repository.UpdateAsync(entity);
            await _distributedCache.RemoveAsync(FormTempMappingCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(FormTempMappingCacheKeys.GetKey(entity.Id));
        }
    }
}
