using CourtApp.Domain.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Repositories
{
    public interface ILocationRepository
    {
        IQueryable<LocationEntity> Entities { get; }
        Task<LocationEntity> GetByIdAsync(Guid id);
        Task<List<LocationEntity>> GetByStateIdAsync(int stateId, int pageNumber, int pageSize);
        Task<int> GetCountByStateIdAsync(int stateId);
        Task DeleteAsync(LocationEntity entity);
        Task<LocationEntity> GetByNameAndStateAsync(string name, int stateId);
        Task<LocationEntity> AddAsync(LocationEntity entity);
        void Update(LocationEntity entity);
    }
}
