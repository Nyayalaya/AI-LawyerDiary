using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.WorkMaster.Services
{
    /// <summary>
    /// Implementation of Work Master service
    /// Provides business logic and data access operations for Work Master entities
    /// </summary>
    public class WorkMasterService : IWorkMasterService
    {
        private readonly IWorkMasterRepository _repository;

        public WorkMasterService(IWorkMasterRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Gets all Work Master entities as queryable for custom filtering
        /// </summary>
        public IQueryable<WorkTypeEntity> WorkMasters => _repository.Entities;

        /// <summary>
        /// Retrieves all Work Master records from the database
        /// </summary>
        public async Task<List<WorkTypeEntity>> GetListAsync()
        {
            return await _repository.GetListAsync();
        }

        /// <summary>
        /// Retrieves a specific Work Master by its ID
        /// </summary>
        public async Task<WorkTypeEntity> GetByIdAsync(Guid workMasterId)
        {
            return await _repository.GetByIdAsync(workMasterId);
        }

        /// <summary>
        /// Inserts a new Work Master record into the database
        /// </summary>
        public async Task<Guid> InsertAsync(WorkTypeEntity workMaster)
        {
            await _repository.InsertAsync(workMaster);
            return workMaster.Id;
        }

        /// <summary>
        /// Updates an existing Work Master record
        /// </summary>
        public async Task UpdateAsync(WorkTypeEntity workMaster)
        {
            await _repository.UpdateAsync(workMaster);
        }

        /// <summary>
        /// Deletes a Work Master record from the database
        /// </summary>
        public async Task DeleteAsync(WorkTypeEntity workMaster)
        {
            await _repository.DeleteAsync(workMaster);
        }

        /// <summary>
        /// Checks if a Work Master with the same name already exists for a specific court type
        /// Used to prevent duplicate entries
        /// </summary>
        public async Task<bool> IsWorkMasterExistAsync(Guid courtTypeId, string name)
        {
            var exists = _repository.Entities
                .Any(w => w.Name.ToLower() == name.ToLower().Trim() 
                    && w.CourtTypeId == courtTypeId);
            
            return await Task.FromResult(exists);
        }
    }
}
