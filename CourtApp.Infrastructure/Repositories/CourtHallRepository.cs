using CourtApp.Application.CacheKeys;
using CourtApp.Application.Features.CourtHall.Interfaces;
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
    public class CourtHallRepository : ICourtHallRepository
    {
        private readonly IRepositoryAsync<CourtHallEntity> _repository;
        private readonly IDistributedCache _distributedCache;

        public CourtHallRepository(IRepositoryAsync<CourtHallEntity> repository, IDistributedCache distributedCache)
        {
            this._repository = repository;
            this._distributedCache = distributedCache;
        }

        public IQueryable<CourtHallEntity> Entities => _repository.Entities;

        public async Task DeleteAsync(CourtHallEntity entity)
        {
            await _repository.DeleteAsync(entity);
            await _distributedCache.RemoveAsync(CourtHallCacheKeys.CourtHallKey);
        }

        public async Task<CourtHallEntity> GetByIdAsync(Guid id)
        {
            var detail = await _repository.Entities
                .Include(d => d.CourtComplex)
                .Include(d => d.Languages)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
            return detail;
        }

        public async Task<List<CourtHallEntity>> GetListAsync()
        {
            return await _repository.Entities
                .Include(d => d.CourtComplex)
                .Include(d => d.Languages)
                .OrderBy(o => o.Name)
                .ToListAsync();
        }

        public async Task<List<CourtHallEntity>> GetByCourtComplexIdAsync(Guid courtComplexId)
        {
            return await _repository.Entities
                .Include(d => d.CourtComplex)
                .Include(d => d.Languages)
                .Where(w => w.CourtComplexId == courtComplexId)
                .OrderBy(o => o.Name)
                .ToListAsync();
        }

        public async Task<Guid> InsertAsync(CourtHallEntity entity)
        {
            await _repository.AddAsync(entity);
            await _distributedCache.RemoveAsync(CourtHallCacheKeys.CourtHallKey);
            return entity.Id;
        }

        public async Task UpdateAsync(CourtHallEntity entity)
        {
            await _repository.UpdateAsync(entity);
            await _distributedCache.RemoveAsync(CourtHallCacheKeys.CourtHallKey);
        }
    }
}
