using CourtApp.Domain.Entities.CaseDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Repositories
{
    public interface ICaseTitleRepository
    {
        IQueryable<CaseTitleEntity> Titles { get; }
        Task<List<CaseTitleEntity>> GetListAsync();
        Task<CaseTitleEntity> GetByIdAsync(Guid TitleId);
        Task<List<CaseTitleEntity>> GetByCaseIdAsync(Guid CaseId);
        Task<Guid> InsertAsync(CaseTitleEntity CaseTtitle);
        Task UpdateAsync(CaseTitleEntity CaseTtitle);
        Task DeleteAsync(CaseTitleEntity CaseTtitle);
    }
}
