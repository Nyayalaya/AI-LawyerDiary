using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourtApp.Application.CacheKeys;
namespace CourtApp.Infrastructure.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly IRepositoryAsync<SubjectEntity> _repository;
        private readonly IDistributedCache _distributedCache;

        public SubjectRepository(IRepositoryAsync<SubjectEntity> _repository, IDistributedCache _distributedCache)
        {
            this._repository = _repository;
            this._distributedCache = _distributedCache;
        }
        public IQueryable<SubjectEntity> Subjects => _repository.Entities;

        public async Task DeleteAsync(SubjectEntity subject)
        {
            await _repository.DeleteAsync(subject);
            await _distributedCache.RemoveAsync(SubjectCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(SubjectCacheKeys.GetKey(subject.Id));
        }

        public async Task<SubjectEntity> GetByIdAsync(Guid Id)
        {
            return await _repository.Entities.Where(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<SubjectEntity>> GetListAsync()
        {
            return await _repository.Entities.OrderByDescending(o => o.Name_En).ToListAsync();
        }

        public async Task<Guid> InsertAsync(SubjectEntity subject)
        {
            await _repository.AddAsync(subject);
            await _distributedCache.RemoveAsync(SubjectCacheKeys.ListKey);
            return subject.Id;
        }

        public async Task UpdateAsync(SubjectEntity subject)
        {
            await _repository.UpdateAsync(subject);
            await _distributedCache.RemoveAsync(SubjectCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(SubjectCacheKeys.GetKey(subject.Id));
        }
    }
}
