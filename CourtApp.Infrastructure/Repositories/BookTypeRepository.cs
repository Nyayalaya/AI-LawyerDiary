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
    public class BookTypeRepository : IBookTypeRepository
    {
        private readonly IRepositoryAsync<BookTypeEntity> _repository;
        private readonly IDistributedCache _distributedCache;

        public BookTypeRepository(IRepositoryAsync<BookTypeEntity> _repository, IDistributedCache _distributedCache)
        {
            this._distributedCache = _distributedCache;
            this._repository = _repository;
        }
        public IQueryable<BookTypeEntity> BookTypes => _repository.Entities;

        public async Task DeleteAsync(BookTypeEntity bookType)
        {
            await _repository.DeleteAsync(bookType);
            await _distributedCache.RemoveAsync(BookTypeCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(BookTypeCacheKeys.GetKey(bookType.Id));
        }

        public async Task<BookTypeEntity> GetByIdAsync(Guid bookTypeId)
        {
            return await _repository.Entities.Where(p => p.Id == bookTypeId).FirstOrDefaultAsync();
        }

        public async Task<List<BookTypeEntity>> GetListAsync()
        {
            return await _repository.Entities.OrderByDescending(o=>o.Id).ToListAsync();
        }

        public async Task<Guid> InsertAsync(BookTypeEntity bookType)
        {
            await _repository.AddAsync(bookType);
            await _distributedCache.RemoveAsync(BookTypeCacheKeys.ListKey);
            return bookType.Id;
        }

        public async Task UpdateAsync(BookTypeEntity bookType)
        {
            await _repository.UpdateAsync(bookType);
            await _distributedCache.RemoveAsync(BookTypeCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(BookTypeCacheKeys.GetKey(bookType.Id));
        }
    }
}
