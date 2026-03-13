using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Repositories
{
    public interface ICaseKindRepository
    {
        IQueryable<CaseKindEntity> QryEntities { get; }

        Task<List<CaseKindEntity>> GetListAsync();

        Task<CaseKindEntity> GetByIdAsync(Guid Id);

        Task<Guid> InsertAsync(CaseKindEntity objEntity);

        Task UpdateAsync(CaseKindEntity objEntity);

        Task DeleteAsync(CaseKindEntity objEntity);
    }
}
