
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using CourtApp.Application.CacheKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Repositories
{
    public class CourtComplexRepository : ICourtComplexRepository
    {
        private readonly IRepositoryAsync<CourtComplexEntity> _repository;
        private readonly IDistributedCache _distributedCache;
        public CourtComplexRepository(IRepositoryAsync<CourtComplexEntity> _repository,
            IDistributedCache _distributedCache)
        {
            this._repository = _repository;
            this._distributedCache = _distributedCache;
        }
        public IQueryable<CourtComplexEntity> Entities => _repository.Entities;

        public async Task DeleteAsync(CourtComplexEntity Entity)
        {
            await _repository.DeleteAsync(Entity);
            await _distributedCache.RemoveAsync(AppCacheKeys.CourtComplexKey);
        }

        public async Task<CourtComplexEntity> GetByIdAsync(Guid Id)
        {
            var DetailById = _repository.Entities
               .Include(d => d.State)
               //.Include(d => d.District)
               .Include(d => d.CourtDistrict)
               .Where(p => p.Id == Id).FirstOrDefaultAsync();
            return await DetailById;
        }

        public async Task<List<CourtComplexEntity>> GetListAsync()
        {
            var dl = _repository.Entities.ToListAsync();
            return await dl;
        }

        public async Task<Guid> InsertAsync(CourtComplexEntity Entity)
        {
            await _repository.AddAsync(Entity);
            await _distributedCache.RemoveAsync(AppCacheKeys.CourtComplexKey);
            return Entity.Id;
        }

        public async Task UpdateAsync(CourtComplexEntity Entity)
        {
            await _repository.UpdateAsync(Entity);
            await _distributedCache.RemoveAsync(AppCacheKeys.CourtComplexKey);
        }
    }
}
