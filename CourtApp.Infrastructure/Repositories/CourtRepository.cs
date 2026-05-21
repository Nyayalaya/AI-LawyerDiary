
using CourtApp.Application.Constants;
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
    public class CourtRepository : ICourtRepository
    {
        private readonly IRepositoryAsync<CourtEntity> _repository;
        private readonly IDistributedCache _distributedCache;

        public CourtRepository(IRepositoryAsync<CourtEntity> repository, IDistributedCache distributedCache)
        {
            _repository = repository;
            _distributedCache = distributedCache;
        }

        public IQueryable<CourtEntity> Entities => _repository.Entities;

        public async Task<CourtEntity> GetByIdAsync(Guid id)
        {
            return await _repository.Entities

                .Include(x => x.CourtType)

                .Include(x => x.Languages)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<CourtEntity>> GetByLocationIdAsync(Guid locationId, int pageNumber, int pageSize)
        {
            return await _repository.Entities

                .Include(x => x.CourtType)

                .Include(x => x.Languages)
                .OrderBy(x => x.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<CourtEntity> GetByNameAndLocationAsync(string name, Guid locationId)
        {
            var nameLower = name?.ToLower();
            return await _repository.Entities

                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<int> GetCountByLocationIdAsync(Guid locationId)
        {
            return await _repository.Entities

                .CountAsync();
        }

        public async Task<CourtEntity> AddAsync(CourtEntity entity)
        {
            await _distributedCache.RemoveAsync(CacheKeys.List<CourtEntity>());
            return await _repository.AddAsync(entity);
        }

        public void Update(CourtEntity entity)
        {
            var dbSet = _repository.Entities as DbSet<CourtEntity>;
            if (dbSet != null)
            {
                dbSet.Update(entity);
            }
        }

        public Task DeleteAsync(CourtEntity entity)
        {
            throw new NotImplementedException();
        }

        public async Task<string> AddRangeAsync(List<CourtEntity> entities)
        {
            await _repository.AddRange(entities);
            await _distributedCache.RemoveAsync(CacheKeys.List<CourtEntity>());
            return entities.FirstOrDefault().Id.ToString();
        }
    }
}
