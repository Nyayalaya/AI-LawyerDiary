using CourtApp.Application.Interfaces.CacheRepositories;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Application.CacheKeys;
using AspNetCoreHero.Extensions.Caching;
using AspNetCoreHero.ThrowR;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Threading.Tasks;
using CourtApp.Domain.Entities.LawyerDiary;
using System;

namespace CourtApp.Infrastructure.CacheRepositories
{
    public class ClientCacheRepository : IClientCacheRepository
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IClientRepository _clientRepository;

        public ClientCacheRepository(IDistributedCache distributedCache, IClientRepository _clientRepository)
        {
            _distributedCache = distributedCache;
           this._clientRepository = _clientRepository;
        }

        public async Task<ClientEntity> GetByIdAsync(Guid clientId)
        {
            string cacheKey = ClientCacheKeys.GetKey(clientId);
            var client = await _distributedCache.GetAsync<ClientEntity>(cacheKey);
            if (client == null)
            {
                client = await _clientRepository.GetByIdAsync(clientId);
                Throw.Exception.IfNull(client, "ClientEntity", "No Client Found");
                await _distributedCache.SetAsync(cacheKey, client);
            }
            return client;
        }

        public async Task<List<ClientEntity>> GetCachedListAsync()
        {
            string cacheKey = ClientCacheKeys.ListKey;
            var clientList = await _distributedCache.GetAsync<List<ClientEntity>>(cacheKey);
            if (clientList==null)
            {
                clientList = await _clientRepository.GetListAsync();
                await _distributedCache.SetAsync(cacheKey, clientList);
            }
            return clientList;
        }       
    }
}