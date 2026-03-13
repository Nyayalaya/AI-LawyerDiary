using CourtApp.Domain.Entities.CaseDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Repositories
{
    public interface ICaseAgainstRepository
    {
        IQueryable<CaseDetailAgainstEntity> Entities { get; }
        Task<List<CaseDetailAgainstEntity>> GetListAsync();
        Task<CaseDetailAgainstEntity> GetByIdAsync(Guid Id);
        Task UpdateAsync(List<CaseDetailAgainstEntity> Entity);
        Task DeleteAsync(CaseDetailAgainstEntity Entity);
    }
}
