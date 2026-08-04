using CourtApp.Domain.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Cadre.Services
{
    public interface ICadreCacheService
    {
        Task<List<CadreEntity>> GetCachedListAsync();
        Task<CadreEntity> GetCachedByIdAsync(Guid cadreId);
        Task InvalidateCacheAsync();
    }
}
