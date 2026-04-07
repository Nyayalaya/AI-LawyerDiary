using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.WorkMasterSub.Services
{
    /// <summary>
    /// Implementation of Work Master Sub service
    /// Provides business logic and data access operations for Work Master Sub entities
    /// </summary>
    public class WorkMasterSubService : IWorkMasterSubService
    {
        private readonly IWorkMasterSubRepository _repository;

        public WorkMasterSubService(IWorkMasterSubRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Gets all Work Master Sub entities as queryable for custom filtering
        /// </summary>
        public IQueryable<WorksEntity> WorkMasterSubs => _repository.Entities;

        /// <summary>
        /// Retrieves all Work Master Sub records from the database
        /// </summary>
        public async Task<List<WorksEntity>> GetListAsync()
        {
            return await _repository.GetListAsync();
        }

        /// <summary>
        /// Retrieves a specific Work Master Sub by its ID
        /// </summary>
        public async Task<WorksEntity> GetByIdAsync(Guid workSubId)
        {
            return await _repository.GetByIdAsync(workSubId);
        }

        /// <summary>
        /// Inserts a new Work Master Sub record into the database
        /// </summary>
        public async Task<Guid> InsertAsync(WorksEntity workSub)
        {
            await _repository.InsertAsync(workSub);
            return workSub.Id;
        }

        /// <summary>
        /// Updates an existing Work Master Sub record
        /// </summary>
        public async Task UpdateAsync(WorksEntity workSub)
        {
            await _repository.UpdateAsync(workSub);
        }

        /// <summary>
        /// Deletes a Work Master Sub record from the database
        /// </summary>
        public async Task DeleteAsync(WorksEntity workSub)
        {
            await _repository.DeleteAsync(workSub);
        }

        /// <summary>
        /// Checks if a Work Master Sub with the same name already exists for a specific work master and court type
        /// Used to prevent duplicate entries
        /// </summary>
        public async Task<bool> IsWorkMasterSubExistAsync(Guid workId, Guid courtTypeId, string name)
        {
            var exists = _repository.Entities
                .Any(w => w.Name.ToLower() == name.ToLower().Trim()
                    && w.WorkId == workId
                    && w.CourtTypeId == courtTypeId);
            
            return await Task.FromResult(exists);
        }
    }
}
