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
    public class TemplateInfoRepository : ITemplateInfoRepository
    {
        private readonly IRepositoryAsync<TemplateInfoEntity> _repository;
        private readonly IDistributedCache _distributedCache;
        public TemplateInfoRepository(IRepositoryAsync<TemplateInfoEntity> _repository,
            IDistributedCache _distributedCache)
        {
            this._repository = _repository;
            this._distributedCache = _distributedCache;
        }
        public IQueryable<TemplateInfoEntity> Entities => _repository.Entities;

        public async Task DeleteAsync(TemplateInfoEntity entity)
        {
            await _repository.DeleteAsync(entity);
            await _distributedCache.RemoveAsync(TemplateInfoCacheKey.ListKey);
            await _distributedCache.RemoveAsync(TemplateInfoCacheKey.GetKey(entity.Id));
        }

        public async Task<TemplateInfoEntity> GetByIdAsync(Guid Id)
        {
            return await _repository.Entities.Where(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<TemplateInfoEntity>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<Guid> InsertAsync(TemplateInfoEntity entity)
        {
            await _repository.AddAsync(entity);
            await _distributedCache.RemoveAsync(TemplateInfoCacheKey.ListKey);
            return entity.Id;
        }

        public async Task UpdateAsync(TemplateInfoEntity entity)
        {
            await _repository.UpdateAsync(entity);
            await _distributedCache.RemoveAsync(TemplateInfoCacheKey.ListKey);
            await _distributedCache.RemoveAsync(TemplateInfoCacheKey.GetKey(entity.Id));
        }
    }
}
