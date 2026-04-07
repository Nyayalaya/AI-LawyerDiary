using CourtApp.Application.DTOs.Location;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.CacheRepositories
{
    public interface ILocationCacheRepository
    {
        Task<LocationResponse> GetAsync(Guid id);
        Task<List<LocationResponse>> GetByStateAsync(int stateId);
        Task<bool> RemoveAsync(Guid id);
    }
}
