using CourtApp.Domain.Entities.CaseDetails;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Repositories
{
    public interface ICaseWorkRepository
    {
        IQueryable<CaseWorkEntity> Entities { get; }
        Task<List<CaseWorkEntity>> GetListAsync();
        Task<List<CaseWorkEntity>> GetWorkByCaseIdAsync(Guid CaseId);
        Task<CaseWorkEntity> GetByIdAsync(Guid Id);
        Task<Guid> InsertAsync(CaseWorkEntity entity);
        Task UpdateAsync(CaseWorkEntity entity);
        Task DeleteAsync(CaseWorkEntity entity);
    }
}
