using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.CaseDetails;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Repositories
{
    public class CaseAgainstRepository : ICaseAgainstRepository
    {
        private readonly IRepositoryAsync<CaseDetailAgainstEntity> _repository;
        private readonly IDistributedCache _distributedCache;
        public CaseAgainstRepository(IRepositoryAsync<CaseDetailAgainstEntity> _repository,
            IDistributedCache _distributedCache)
        {
            this._repository = _repository;
            this._distributedCache = _distributedCache;
        }

        public IQueryable<CaseDetailAgainstEntity> Entities => _repository.Entities;

        public Task DeleteAsync(CaseDetailAgainstEntity Entity)
        {
            throw new NotImplementedException();
        }

        public Task<CaseDetailAgainstEntity> GetByIdAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<CaseDetailAgainstEntity>> GetListAsync()
        {
            throw new NotImplementedException();
        }

        //public async Task<Guid> InsertAsync(List<CaseDetailAgainstEntity> Entity)
        //{
        //    await _repository.BulkInsert(Entity);
        //    await _distributedCache.RemoveAsync(AppCacheKeys.CourtComplexKey);
        //    return Entity.Select(s => s.CaseId).FirstOrDefault();
        //}

        public Task UpdateAsync(List<CaseDetailAgainstEntity> Entity)
        {
            throw new NotImplementedException();
        }
    }
}
