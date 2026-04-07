using CourtApp.Domain.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Cadre.Services
{
    public interface ICadreCacheService
    {
        Task<List<CadreMasterEntity>> GetCachedListAsync();
        Task<CadreMasterEntity> GetCachedByIdAsync(Guid cadreId);
        Task InvalidateCacheAsync();
    }
}
