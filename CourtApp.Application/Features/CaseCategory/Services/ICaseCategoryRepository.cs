using CourtApp.Domain.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseCategory.Services
{
    public interface ICaseCategoryRepository
    {
        IQueryable<CaseCategoryEntity> CaseNatures { get; }

        Task<List<CaseCategoryEntity>> GetListAsync();

        Task<CaseCategoryEntity> GetByIdAsync(Guid caseNatureId);

        Task<Guid> InsertAsync(CaseCategoryEntity caseNature);

        Task UpdateAsync(CaseCategoryEntity caseNature);

        Task DeleteAsync(CaseCategoryEntity caseNature);

        Task<bool> IsCaseCategoryExistAsync(Guid courtTypeId, string category);
    }
}
