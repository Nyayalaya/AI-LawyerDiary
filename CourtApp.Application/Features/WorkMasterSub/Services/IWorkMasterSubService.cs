using CourtApp.Domain.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.WorkMasterSub.Services
{
    /// <summary>
    /// Service interface for Work Master Sub repository operations
    /// Encapsulates business logic and data access for Work Master Sub entities
    /// </summary>
    public interface IWorkMasterSubService
    {
        /// <summary>
        /// Gets all Work Master Sub entities as queryable
        /// </summary>
        IQueryable<WorksEntity> WorkMasterSubs { get; }

        /// <summary>
        /// Retrieves all Work Master Sub records
        /// </summary>
        Task<List<WorksEntity>> GetListAsync();

        /// <summary>
        /// Retrieves a specific Work Master Sub by ID
        /// </summary>
        Task<WorksEntity> GetByIdAsync(Guid workSubId);

        /// <summary>
        /// Inserts a new Work Master Sub record
        /// </summary>
        Task<Guid> InsertAsync(WorksEntity workSub);

        /// <summary>
        /// Updates an existing Work Master Sub record
        /// </summary>
        Task UpdateAsync(WorksEntity workSub);

        /// <summary>
        /// Deletes a Work Master Sub record
        /// </summary>
        Task DeleteAsync(WorksEntity workSub);

        /// <summary>
        /// Checks if a Work Master Sub with the same name already exists for a specific work master and court type
        /// </summary>
        Task<bool> IsWorkMasterSubExistAsync(Guid workId, Guid courtTypeId, string name);
    }
}

