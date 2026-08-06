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
    public class LocationRepository : ILocationRepository
    {
        private readonly IRepositoryAsync<LocationEntity> _repository;
        private readonly IDistributedCache _distributedCache;

        public LocationRepository(IRepositoryAsync<LocationEntity> repository, IDistributedCache distributedCache)
        {
            _repository = repository;
            _distributedCache = distributedCache;
        }

        public IQueryable<LocationEntity> Entities => _repository.Entities;

        public async Task<LocationEntity> GetByIdAsync(Guid id)
        {
            return await _repository.Entities
                .Include(x => x.State)
                .Include(x => x.ParentLocation)
                .Include(x => x.Languages)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<LocationEntity>> GetByStateIdAsync(int stateId, int pageNumber, int pageSize)
        {
            return await _repository.Entities
                .Where(x => x.StateId == stateId)
                .Include(x => x.State)
                .Include(x => x.Languages)
                .OrderBy(x => x.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<LocationEntity> GetByNameAndStateAsync(string name, int stateId)
        {
            var nameLower = name?.ToLower();
            return await _repository.Entities
                .Where(x => x.StateId == stateId && x.Name.ToLower() == nameLower)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<int> GetCountByStateIdAsync(int stateId)
        {
            return await _repository.Entities
                .Where(x => x.StateId == stateId)
                .CountAsync();
        }

        public async Task<LocationEntity> AddAsync(LocationEntity entity)
        {
            return await _repository.AddAsync(entity);
        }

        public void Update(LocationEntity entity)
        {
            var dbSet = _repository.Entities as DbSet<LocationEntity>;
            if (dbSet != null)
            {
                dbSet.Update(entity);
            }
        }

        public Task DeleteAsync(LocationEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
