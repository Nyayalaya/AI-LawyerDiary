using CourtApp.Domain.Entities.CaseDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Repositories
{
    public interface IUserCaseRepository
    {
        IQueryable<CaseDetailEntity> Entites { get; }
        Task<List<CaseDetailEntity>> GetListAsync();
        Task<CaseDetailEntity> GetByIdAsync(Guid Id);
        Task<CaseDetailEntity> GetMostRecentCaseInfo(List<string> LinkedIds);
        Task<CaseDetailEntity> GetDetailAsync(Guid Id);
        Task<Guid> InsertAsync(CaseDetailEntity caseEntity);
        Task UpdateAsync(CaseDetailEntity caseEntity);
        Task DeleteAsync(CaseDetailEntity caseEntity);
        Task<CaseDetailEntity> GetByCaseNoYearAsync(string No, int Year);
        Task UpdateRangeAsync(List<CaseDetailEntity> casesToUpdate);
    }
}
