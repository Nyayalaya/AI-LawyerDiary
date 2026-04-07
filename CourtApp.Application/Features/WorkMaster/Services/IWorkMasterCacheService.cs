using CourtApp.Domain.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.WorkMaster.Services
{
    /// <summary>
    /// Service interface for Work Master cache operations
    /// Provides cached access to frequently used Work Master data
    /// Improves performance by reducing database queries
    /// </summary>
    public interface IWorkMasterCacheService
    {
        /// <summary>
        /// Retrieves all Work Master records from cache
        /// Falls back to database if cache is empty
        /// </summary>
        Task<List<WorkTypeEntity>> GetCachedListAsync();

        /// <summary>
        /// Retrieves a specific Work Master by ID from cache
        /// </summary>
        Task<WorkTypeEntity> GetByIdAsync(Guid workMasterId);

        /// <summary>
        /// Invalidates the Work Master cache
        /// Called after Create, Update, or Delete operations
        /// </summary>
        Task InvalidateCacheAsync();
    }
}
