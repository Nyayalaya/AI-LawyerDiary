using CourtApp.Application.Enums;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Application.CacheKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourtApp.Domain.Entities.Masters;

namespace CourtApp.Infrastructure.Repositories
{
    public class ProceedingHeadRepository : IProceedingHeadRepository
    {
        private readonly IRepositoryAsync<ProceedingTypeEntity> _repository;
        private readonly IDistributedCache _distributedCache;
        public ProceedingHeadRepository(IRepositoryAsync<ProceedingTypeEntity> _repository, IDistributedCache _distributedCache)
        {
            this._repository = _repository;
            this._distributedCache = _distributedCache;
        }

        public IQueryable<ProceedingTypeEntity> Entities => _repository.Entities;

        public async Task DeleteAsync(ProceedingTypeEntity proceedingHeadEntity)
        {
            await _repository.DeleteAsync(proceedingHeadEntity);
            await _distributedCache.RemoveAsync(AppCacheKeys.ProcHeadKey);
        }

        public async Task<ProceedingTypeEntity> GetByIdAsync(Guid Id)
        {
            var DetailDt = await _repository.Entities
                .Where(c => c.Id == Id).FirstOrDefaultAsync();
            return DetailDt;
        }

        public async Task<List<ProceedingTypeEntity>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<Guid> InsertAsync(ProceedingTypeEntity proceedingHeadEntity)
        {
            await _repository.AddAsync(proceedingHeadEntity);
            await _distributedCache.RemoveAsync(AppCacheKeys.ProcHeadKey);
            return proceedingHeadEntity.Id;
        }

        public async Task UpdateAsync(ProceedingTypeEntity proceedingHeadEntity)
        {
            await _repository.UpdateAsync(proceedingHeadEntity);
        }
    }
}
