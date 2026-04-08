using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Application.CacheKeys;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CourtApp.Domain.Entities.Masters;

namespace CourtApp.Infrastructure.Repositories
{
    public class SpecilityRepository : ISpecilityRepository
    {
        private readonly IRepositoryAsync<Specialization> _repository;
        private readonly IDistributedCache _distributedCache;
        public SpecilityRepository(IRepositoryAsync<Specialization> _repository, IDistributedCache _distributedCache)
        {
            this._distributedCache = _distributedCache;
            this._repository = _repository;
        }

        public IQueryable<Specialization> Entities => _repository.Entities;

        public async Task DeleteAsync(Specialization entity)
        {
            await _repository.DeleteAsync(entity);
            await _distributedCache.RemoveAsync(SpecilityCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(SpecilityCacheKeys.GetKey(entity.Id));
        }

        public async Task<Specialization> GetByIdAsync(Guid Id)
        {
            return await _repository
                .Entities
                .Where(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<Specialization>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<Guid> InsertAsync(Specialization entity)
        {
            await _repository.AddAsync(entity);
            await _distributedCache.RemoveAsync(SpecilityCacheKeys.ListKey);
            return entity.Id;
        }

        public async Task UpdateAsync(Specialization entity)
        {
            await _repository.UpdateAsync(entity);
            await _distributedCache.RemoveAsync(SpecilityCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(SpecilityCacheKeys.GetKey(entity.Id));
        }
    }
}
