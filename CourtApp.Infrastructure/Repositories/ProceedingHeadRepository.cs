using CourtApp.Application.Enums;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using CourtApp.Application.CacheKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Repositories
{
    public class ProceedingHeadRepository : IProceedingHeadRepository
    {
        private readonly IRepositoryAsync<ProceedingHeadEntity> _repository;
        private readonly IDistributedCache _distributedCache;
        public ProceedingHeadRepository(IRepositoryAsync<ProceedingHeadEntity> _repository, IDistributedCache _distributedCache)
        {
            this._repository = _repository;
            this._distributedCache = _distributedCache;
        }

        public IQueryable<ProceedingHeadEntity> Entities => _repository.Entities;

        public async Task DeleteAsync(ProceedingHeadEntity proceedingHeadEntity)
        {
            await _repository.DeleteAsync(proceedingHeadEntity);
            await _distributedCache.RemoveAsync(AppCacheKeys.ProcHeadKey);
        }

        public async Task<ProceedingHeadEntity> GetByIdAsync(Guid Id)
        {
            var DetailDt = await _repository.Entities
                .Where(c => c.Id == Id).FirstOrDefaultAsync();
            return DetailDt;
        }

        public async Task<List<ProceedingHeadEntity>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<Guid> InsertAsync(ProceedingHeadEntity proceedingHeadEntity)
        {
            await _repository.AddAsync(proceedingHeadEntity);
            await _distributedCache.RemoveAsync(AppCacheKeys.ProcHeadKey);
            return proceedingHeadEntity.Id;
        }

        public async Task UpdateAsync(ProceedingHeadEntity proceedingHeadEntity)
        {
            await _repository.UpdateAsync(proceedingHeadEntity);
        }
    }
}
