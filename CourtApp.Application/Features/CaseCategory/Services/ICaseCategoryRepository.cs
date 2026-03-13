using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseCategory.Services
{
    public interface ICaseCategoryRepository
    {
        IQueryable<NatureEntity> CaseNatures { get; }

        Task<List<NatureEntity>> GetListAsync();

        Task<NatureEntity> GetByIdAsync(Guid caseNatureId);

        Task<Guid> InsertAsync(NatureEntity caseNature);

        Task UpdateAsync(NatureEntity caseNature);

        Task DeleteAsync(NatureEntity caseNature);

        Task<bool> IsCaseCategoryExistAsync(Guid courtTypeId, string category);
    }
}
