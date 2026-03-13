using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Repositories
{
    public interface ICourtDistrictRepository
    {
        IQueryable<CourtDistrictEntity> Entities { get; }
        Task<List<CourtDistrictEntity>> GetListAsync();
        Task<CourtDistrictEntity> GetByIdAsync(Guid Id);
        Task<Guid> InsertAsync(CourtDistrictEntity Entity);
        Task<Guid> InsertRangeAsync(List<CourtDistrictEntity> entities);
        Task UpdateAsync(CourtDistrictEntity Entity);
        Task DeleteAsync(CourtDistrictEntity Entity);
    }
}
