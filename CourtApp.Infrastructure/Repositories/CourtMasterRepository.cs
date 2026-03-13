using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.Common;
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
    public class CourtMasterRepository : ICourtMasterRepository
    {
        private readonly IRepositoryAsync<CourtMasterEntity> _repository;
        private readonly IDistributedCache _distributedCache;

        public CourtMasterRepository(IRepositoryAsync<CourtMasterEntity> _repository,
            IDistributedCache _distributedCache)
        {
            this._repository = _repository;
            this._distributedCache = _distributedCache;
        }

        public IQueryable<CourtMasterEntity> QryEntities => _repository.Entities;

        public IQueryable<CourtMasterEntity> Entities => _repository.Entities;

        public async Task DeleteAsync(CourtMasterEntity objEntity)
        {
            await _repository.DeleteAsync(objEntity);
            await _distributedCache.RemoveAsync(AppCacheKeys.CourtMasterKey);
        }

        public async Task<CourtMasterEntity> GetByIdAsync(Guid Id)
        {
            var DetailById = _repository.Entities
                .Include(d=>d.CourtBenches)               
                .Where(p => p.Id == Id).FirstOrDefaultAsync();

            return await DetailById;
        }

        public async Task<List<CourtMasterEntity>> GetListAsync()
        {
            return await _repository.Entities.OrderByDescending(o => o.Name_En).ToListAsync();
        }

        public async Task<Guid> InsertAsync(CourtMasterEntity objEntity)
        {
            await _repository.AddAsync(objEntity);
            await _distributedCache.RemoveAsync(AppCacheKeys.CourtMasterKey);
            return objEntity.Id;
        }

        public async Task UpdateAsync(CourtMasterEntity objEntity)
        {
            await _repository.UpdateAsync(objEntity);
            await _distributedCache.RemoveAsync(AppCacheKeys.CourtMasterKey);
        }
    }
}
