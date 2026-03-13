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
    public class LawyerMasterRepository : ILawyerRepository
    {
        private readonly IRepositoryAsync<LawyerMasterEntity> _repository;
        private readonly IDistributedCache _distributedCache;

        public LawyerMasterRepository(IRepositoryAsync<LawyerMasterEntity> _repository, 
            IDistributedCache _distributedCache)
        {
            this._distributedCache = _distributedCache;
            this._repository = _repository;
        }
        public IQueryable<LawyerMasterEntity> Entities => _repository.Entities;

        public async Task DeleteAsync(LawyerMasterEntity lawyer)
        {
            await _repository.DeleteAsync(lawyer);
            await _distributedCache.RemoveAsync(LawyerMasterCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(LawyerMasterCacheKeys.GetKey(lawyer.Id));
        }

        public async Task<LawyerMasterEntity> GetByIdAsync(Guid Id)
        {
            return await _repository.Entities.Where(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<LawyerMasterEntity>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<Guid> InsertAsync(LawyerMasterEntity lawyer)
        {
            await _repository.AddAsync(lawyer);
            await _distributedCache.RemoveAsync(LawyerMasterCacheKeys.ListKey);
            return lawyer.Id;
        }

        public async Task UpdateAsync(LawyerMasterEntity lawyer)
        {
            await _repository.UpdateAsync(lawyer);
            await _distributedCache.RemoveAsync(LawyerMasterCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(LawyerMasterCacheKeys.GetKey(lawyer.Id));
        }
    }
}
