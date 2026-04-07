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
                .Include(x => x.Location)
                .Include(x => x.CourtType)
                .Include(x => x.CourtLevel)
                .Include(x => x.Languages)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<CourtEntity>> GetByLocationIdAsync(Guid locationId, int pageNumber, int pageSize)
        {
            return await _repository.Entities
                .Where(x => x.LocationId == locationId)
                .Include(x => x.Location)
                .Include(x => x.CourtType)
                .Include(x => x.CourtLevel)
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
                .Where(x => x.LocationId == locationId && x.Name.ToLower() == nameLower)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<int> GetCountByLocationIdAsync(Guid locationId)

        public async Task<CourtEntity> AddAsync(CourtEntity entity)
        {
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
    }
}
