using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Application.CacheKeys;
using CourtApp.Domain.Entities.LawyerDiary;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CourtApp.Infrastructure.Repositories
{
    public class SpecilityRepository : ISpecilityRepository
    {
        private readonly IRepositoryAsync<SpecializationEntity> _repository;
        private readonly IDistributedCache _distributedCache;
        public SpecilityRepository(IRepositoryAsync<SpecializationEntity> _repository, IDistributedCache _distributedCache)
        {
            this._distributedCache = _distributedCache;
            this._repository = _repository;
        }

        public IQueryable<SpecializationEntity> Entities => _repository.Entities;

        public async Task DeleteAsync(SpecializationEntity entity)
        {
            await _repository.DeleteAsync(entity);
            await _distributedCache.RemoveAsync(SpecilityCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(SpecilityCacheKeys.GetKey(entity.Id));
        }

        public async Task<SpecializationEntity> GetByIdAsync(Guid Id)
        {
            return await _repository
                .Entities
                .Where(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<SpecializationEntity>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<Guid> InsertAsync(SpecializationEntity entity)
        {
            await _repository.AddAsync(entity);
            await _distributedCache.RemoveAsync(SpecilityCacheKeys.ListKey);
            return entity.Id;
        }

        public async Task UpdateAsync(SpecializationEntity entity)
        {
            await _repository.UpdateAsync(entity);
            await _distributedCache.RemoveAsync(SpecilityCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(SpecilityCacheKeys.GetKey(entity.Id));
        }
    }
}
