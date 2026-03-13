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
    public class PublisheRepository : IPublicationRepository
    {
        private readonly IRepositoryAsync<PublisherEntity> _repository;
        private readonly IDistributedCache _distributedCache;
        public PublisheRepository(IRepositoryAsync<PublisherEntity> _repository, IDistributedCache _distributedCache)
        {
            this._repository = _repository;
            this._distributedCache = _distributedCache;

        }
        public IQueryable<PublisherEntity> BookTypes => _repository.Entities;

        public async Task DeleteAsync(PublisherEntity obj)
        {
            await _repository.DeleteAsync(obj);
            await _distributedCache.RemoveAsync(PublisherCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(PublisherCacheKeys.GetKey(obj.Id));
        }

        public async Task<PublisherEntity> GetByIdAsync(Guid Id)
        {
            return await _repository.Entities.Where(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<PublisherEntity>> GetListAsync()
        {
            return await _repository.Entities.OrderByDescending(o => o.Id).ToListAsync();
        }

        public async Task<Guid> InsertAsync(PublisherEntity obj)
        {
            await _repository.AddAsync(obj);
            await _distributedCache.RemoveAsync(PublisherCacheKeys.ListKey);
            return obj.Id;
        }

        public async Task UpdateAsync(PublisherEntity obj)
        {
            await _repository.UpdateAsync(obj);
            await _distributedCache.RemoveAsync(PublisherCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(PublisherCacheKeys.GetKey(obj.Id));
        }
    }
}
