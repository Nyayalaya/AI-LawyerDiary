using CourtApp.Application.CacheKeys;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.CaseDetails;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Repositories
{
    public class CaseTitleRepository : ICaseTitleRepository
    {
        private readonly IRepositoryAsync<CaseTitleEntity> _repository;
        private readonly IDistributedCache _distributedCache;

        public CaseTitleRepository(IRepositoryAsync<CaseTitleEntity> _repository, IDistributedCache _distributedCache)
        {
            this._repository = _repository;
            this._distributedCache = _distributedCache;
        }
        public IQueryable<CaseTitleEntity> Titles => _repository.Entities;

        public async Task DeleteAsync(CaseTitleEntity CaseTtitle)
        {
            await _repository.DeleteAsync(CaseTtitle);
            await _distributedCache.RemoveAsync(CaseTitleCacheKeys.ListKey);
        }
        public async Task<List<CaseTitleEntity>> GetByCaseIdAsync(Guid CaseId)
        {
            var DetailById = _repository.Entities
                .Include(d => d.Case)
                .Where(p => p.Case.Id == CaseId).ToListAsync();
            return await DetailById;
        }

        public async Task<CaseTitleEntity> GetByIdAsync(Guid TitleId)
        {
            var DetailById = _repository.Entities
            .Include(d => d.Case)
            .Where(p => p.Id == TitleId).FirstOrDefaultAsync();
            return await DetailById;
        }

        public async Task<List<CaseTitleEntity>> GetListAsync()
        {
            var data = _repository.Entities
                .Include(d => d.Case)
                .ToListAsync();
            return await data;
        }

        public async Task<Guid> InsertAsync(CaseTitleEntity CaseTtitle)
        {
            await _repository.AddAsync(CaseTtitle);
            await _distributedCache.RemoveAsync(CaseTitleCacheKeys.ListKey);
            return CaseTtitle.Id;
        }
        public async Task UpdateAsync(CaseTitleEntity CaseTtitle)
        {
            await _repository.UpdateAsync(CaseTtitle);
            await _distributedCache.RemoveAsync(CaseTitleCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CaseTitleCacheKeys.GetKey(CaseTtitle.Id));
        }

        //public async Task BulkInsertAsync(List<CaseTitleEntity> titles)
        //{
        //    await _repository.BulkInsert(titles);
        //}
    }
}
