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
    public class CourtDistrictRepository : ICourtDistrictRepository
    {
        private readonly IRepositoryAsync<CourtDistrictEntity> _repository;
        private readonly IDistributedCache _distributedCache;
        public CourtDistrictRepository(IRepositoryAsync<CourtDistrictEntity> _repository, IDistributedCache _distributedCache)
        {
            this._repository = _repository;
            this._distributedCache = _distributedCache;
        }
        public IQueryable<CourtDistrictEntity> Entities => _repository.Entities;

        public async Task DeleteAsync(CourtDistrictEntity Entity)
        {
            await _repository.DeleteAsync(Entity);
            await _distributedCache.RemoveAsync(CourtDistrictCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CourtDistrictCacheKeys.GetByIdKey(Entity.Id));
        }

        public async Task<CourtDistrictEntity> GetByIdAsync(Guid Id)
        {
            var DetailById = _repository.Entities
               .Include(d => d.State)
               .Where(p => p.Id == Id).FirstOrDefaultAsync();
            return await DetailById;
        }

        public async Task<List<CourtDistrictEntity>> GetListAsync()
        {
            var dl = await _repository.Entities.ToListAsync();
            return dl;
        }

        public async Task<Guid> InsertAsync(CourtDistrictEntity Entity)
        {
            await _repository.AddAsync(Entity);
            await _distributedCache.RemoveAsync(CourtDistrictCacheKeys.ListKey);
            return Entity.Id;
        }

        public async Task<Guid> InsertRangeAsync(List<CourtDistrictEntity> entities)
        {
            await _repository.AddRange(entities);
            await _distributedCache.RemoveAsync(CourtDistrictCacheKeys.ListKey);
            return entities.FirstOrDefault().Id;
        }

        public async Task UpdateAsync(CourtDistrictEntity Entity)
        {
            await _repository.UpdateAsync(Entity);
            await _distributedCache.RemoveAsync(CourtDistrictCacheKeys.ListKey);
        }
    }
}
