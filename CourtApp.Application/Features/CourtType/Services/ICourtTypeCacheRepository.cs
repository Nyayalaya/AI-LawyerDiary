using CourtApp.Application.Common;
using CourtApp.Application.Features.CourtType.Query;
using CourtApp.Domain.Entities;
using CourtApp.Domain.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtType.Services
{
    public interface ICourtTypeCacheRepository
    {
        /// <summary>
        /// Get court type by ID from cache or database
        /// </summary>
        Task<CourtTypeEntity> GetByIdAsync(Guid courtTypeId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get all court types from cache or database
        /// </summary>
        Task<List<CourtTypeEntity>> GetCachedListAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Get mapped response DTOs from cache
        /// </summary>
        Task<List<GetCourtTypeResponse>> GetCachedMappedListAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Get court type dropdown data from cache or database
        /// </summary>
        Task<List<Dropdown>> GetDropdownAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Invalidate all court type caches
        /// </summary>
        Task InvalidateAllCachesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Invalidate specific court type cache by ID
        /// </summary>
        Task InvalidateCacheByIdAsync(Guid courtTypeId, CancellationToken cancellationToken = default);
    }
}
