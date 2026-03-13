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
    public class CaseKindRepository : ICaseKindRepository
    {
        private readonly IRepositoryAsync<CaseKindEntity> _repository;
        private readonly IDistributedCache _distributedCache;


        public CaseKindRepository(IRepositoryAsync<CaseKindEntity> _repository, IDistributedCache _distributedCache)
        {
            this._repository = _repository;
            this._distributedCache = _distributedCache;
        }
        public IQueryable<CaseKindEntity> QryEntities => _repository.Entities;

        public async Task DeleteAsync(CaseKindEntity objEntity)
        {
            await _repository.DeleteAsync(objEntity);
            await _distributedCache.RemoveAsync(CaseKindCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CaseKindCacheKeys.GetKey(objEntity.Id));
        }

        public async Task<CaseKindEntity> GetByIdAsync(Guid Id)
        {
            return await _repository.Entities.Where(ck => ck.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<CaseKindEntity>> GetListAsync()
        {
            return await _repository.Entities.OrderByDescending(o => o.Id).ToListAsync();
        }

        public async Task<Guid> InsertAsync(CaseKindEntity objEntity)
        {
            await _repository.AddAsync(objEntity);
            await _distributedCache.RemoveAsync(CaseKindCacheKeys.ListKey);
            return objEntity.Id;
        }

        public async Task UpdateAsync(CaseKindEntity objEntity)
        {
            await _repository.UpdateAsync(objEntity);
            await _distributedCache.RemoveAsync(CaseKindCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CaseKindCacheKeys.GetKey(objEntity.Id));
        }
    }
}
