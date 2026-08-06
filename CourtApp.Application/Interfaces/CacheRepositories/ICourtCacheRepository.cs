using CourtApp.Application.Features.Court.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.CacheRepositories
{
    public interface ICourtCacheRepository
    {
        Task<CourtResponse> GetAsync(Guid id);
        Task<List<CourtResponse>> GetByLocationAsync(Guid locationId);
        Task<bool> RemoveAsync(Guid id);
    }
}
