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
    public class ClientRepository : IClientRepository
    {
        private readonly IRepositoryAsync<ClientEntity> _repository;
        private readonly IDistributedCache _distributedCache;

        public ClientRepository(IRepositoryAsync<ClientEntity> _repository, IDistributedCache _distributedCache)
        {
            this._distributedCache = _distributedCache;
            this._repository = _repository;
        }
        public IQueryable<ClientEntity> Clients => _repository.Entities;

        public async Task DeleteAsync(ClientEntity client)
        {
            await _repository.DeleteAsync(client);
            await _distributedCache.RemoveAsync(ClientCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(ClientCacheKeys.GetKey(client.Id));
        }

        public async Task<ClientEntity> GetByIdAsync(Guid clientId)
        {
            var Detail = await _repository.Entities
                .Where(p => p.Id == clientId).FirstOrDefaultAsync();
            return Detail;
        }

        public async Task<List<ClientEntity>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<Guid> InsertAsync(ClientEntity client)
        {
            await _repository.AddAsync(client);
            await _distributedCache.RemoveAsync(ClientCacheKeys.ListKey);
            return client.Id;
        }

        public async Task UpdateAsync(ClientEntity client)
        {
            await _repository.UpdateAsync(client);
            await _distributedCache.RemoveAsync(ClientCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(ClientCacheKeys.GetKey(client.Id));
        }
    }
}
