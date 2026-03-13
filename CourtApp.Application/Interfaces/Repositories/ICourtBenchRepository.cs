using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Repositories
{
    public interface ICourtBenchRepository
    {
        IQueryable<CourtBenchEntity> Entities { get; }
        Task<List<CourtBenchEntity>> GetListAsync();
        Task<CourtBenchEntity> GetByIdAsync(Guid Id);
        Task<Guid> AddBenchAsync(CourtBenchEntity Entity);
        Task UpdateAsync(List<CourtBenchEntity> Entity);
        Task DeleteAsync(CourtBenchEntity Entity);
    }
}
