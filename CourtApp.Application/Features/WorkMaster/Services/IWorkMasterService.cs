using CourtApp.Domain.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.WorkMaster.Services
{
    /// <summary>
    /// Service interface for Work Master repository operations
    /// Encapsulates business logic and data access for Work Master entities
    /// </summary>
    public interface IWorkMasterService
    {
        /// <summary>
        /// Gets all Work Master entities as queryable
        /// </summary>
        IQueryable<WorkTypeEntity> WorkMasters { get; }

        /// <summary>
        /// Retrieves all Work Master records
        /// </summary>
        Task<List<WorkTypeEntity>> GetListAsync();

        /// <summary>
        /// Retrieves a specific Work Master by ID
        /// </summary>
        Task<WorkTypeEntity> GetByIdAsync(Guid workMasterId);

        /// <summary>
        /// Inserts a new Work Master record
        /// </summary>
        Task<Guid> InsertAsync(WorkTypeEntity workMaster);

        /// <summary>
        /// Updates an existing Work Master record
        /// </summary>
        Task UpdateAsync(WorkTypeEntity workMaster);

        /// <summary>
        /// Deletes a Work Master record
        /// </summary>
        Task DeleteAsync(WorkTypeEntity workMaster);

        /// <summary>
        /// Checks if a Work Master with the same name already exists for a court type
        /// </summary>
        Task<bool> IsWorkMasterExistAsync(Guid courtTypeId, string name);
    }
}

