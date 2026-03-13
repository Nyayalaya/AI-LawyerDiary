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
    public class CourtBenchRepository : ICourtBenchRepository
    {
        private readonly IRepositoryAsync<CourtBenchEntity> _repository;
        private readonly IDistributedCache _distributedCache;
        public CourtBenchRepository(IRepositoryAsync<CourtBenchEntity> _repository,
            IDistributedCache _distributedCache)
        {
            this._repository = _repository;
            this._distributedCache = _distributedCache;
        }
        public IQueryable<CourtBenchEntity> Entities => _repository.Entities;

        public async Task<Guid> AddBenchAsync(CourtBenchEntity Entity)
        {
            await _repository.AddAsync(Entity);
            return Entity.Id;

        }

        public Task DeleteAsync(CourtBenchEntity Entity)
        {
            throw new NotImplementedException();
        }

        public async Task<CourtBenchEntity> GetByIdAsync(Guid Id)
        {
            return await _repository.Entities.Where(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public Task<List<CourtBenchEntity>> GetListAsync()
        {
            return _repository.Entities.ToListAsync();
        }

        //public async Task<Guid> InsertAsync(List<CourtBenchEntity> Entity)
        //{
        //    await _repository.BulkInsert(Entity);
        //    await _distributedCache.RemoveAsync(AppCacheKeys.CourtComplexKey);
        //    return Entity.Select(s => s.Id).FirstOrDefault();
        //}

        public Task UpdateAsync(List<CourtBenchEntity> Entity)
        {
            throw new NotImplementedException();
        }
    }
}
