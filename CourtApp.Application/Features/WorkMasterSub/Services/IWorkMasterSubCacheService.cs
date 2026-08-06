using CourtApp.Domain.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.WorkMasterSub.Services
{
    /// <summary>
    /// Service interface for Work Master Sub cache operations
    /// Provides cached access to frequently used Work Master Sub data
    /// Improves performance by reducing database queries
    /// </summary>
    public interface IWorkMasterSubCacheService
    {
        /// <summary>
        /// Retrieves all Work Master Sub records from cache
        /// Falls back to database if cache is empty
        /// </summary>
        Task<List<WorksEntity>> GetCachedListAsync();

        /// <summary>
        /// Retrieves a specific Work Master Sub by ID from cache
        /// </summary>
        Task<WorksEntity> GetByIdAsync(Guid workSubId);

        /// <summary>
        /// Invalidates the Work Master Sub cache
        /// Called after Create, Update, or Delete operations
        /// </summary>
        Task InvalidateCacheAsync();
    }
}
