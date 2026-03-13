using CourtApp.Application.CacheKeys;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Repositories
{
    public class BookMasterRepository : IBookMasterRepository
    {
        private readonly IRepositoryAsync<LDBookEntity> _repository;
        private readonly IDistributedCache _distributedCache;

        public BookMasterRepository(IRepositoryAsync<LDBookEntity> _repository, IDistributedCache _distributedCache)
        {
            this._distributedCache = _distributedCache;
            this._repository = _repository;
        }
        public IQueryable<LDBookEntity> BookMasters => _repository.Entities;

        public async Task DeleteAsync(LDBookEntity bookMaster)
        {
            await _repository.DeleteAsync(bookMaster);
            await _distributedCache.RemoveAsync(BookMasterCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(BookMasterCacheKeys.GetKey(bookMaster.Id));
        }

        public async Task<LDBookEntity> GetByIdAsync(Guid bookTypeId)
        {
            return await _repository.Entities.Where(p => p.Id == bookTypeId).FirstOrDefaultAsync();
        }

        public async Task<List<LDBookEntity>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<Guid> InsertAsync(LDBookEntity bookMaster)
        {
            await _repository.AddAsync(bookMaster);
            await _distributedCache.RemoveAsync(BookMasterCacheKeys.ListKey);
            return bookMaster.Id;
        }

        public async Task UpdateAsync(LDBookEntity bookMaster)
        {
            await _repository.UpdateAsync(bookMaster);
            await _distributedCache.RemoveAsync(BookMasterCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(BookMasterCacheKeys.GetKey(bookMaster.Id));
        }
    }
}
