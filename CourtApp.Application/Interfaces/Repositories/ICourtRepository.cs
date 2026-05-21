using CourtApp.Domain.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Repositories
{
    public interface ICourtRepository
    {
        IQueryable<CourtEntity> Entities { get; }
        Task<CourtEntity> GetByIdAsync(Guid id);
        Task<List<CourtEntity>> GetByLocationIdAsync(Guid locationId, int pageNumber, int pageSize);
        Task<int> GetCountByLocationIdAsync(Guid locationId);
        Task DeleteAsync(CourtEntity entity);
        Task<CourtEntity> GetByNameAndLocationAsync(string name, Guid locationId);
        Task<CourtEntity> AddAsync(CourtEntity entity);
        Task<string> AddRangeAsync(List<CourtEntity> entities);
        void Update(CourtEntity entity);
    }
}
