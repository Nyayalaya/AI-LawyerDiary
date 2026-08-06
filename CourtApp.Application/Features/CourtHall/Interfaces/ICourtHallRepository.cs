using CourtApp.Domain.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtHall.Interfaces
{
    public interface ICourtHallRepository
    {
        IQueryable<CourtHallEntity> Entities { get; }
        Task<CourtHallEntity> GetByIdAsync(Guid id);
        Task<List<CourtHallEntity>> GetListAsync();
        Task<List<CourtHallEntity>> GetByCourtComplexIdAsync(Guid courtComplexId);
        Task<Guid> InsertAsync(CourtHallEntity entity);
        Task UpdateAsync(CourtHallEntity entity);
        Task DeleteAsync(CourtHallEntity entity);
    }
}
