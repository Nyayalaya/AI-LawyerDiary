using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Repositories
{
    public interface ICaseStageRepository
    {
        IQueryable<CaseStageEntity> QryEntities { get; }

        Task<List<CaseStageEntity>> GetListAsync();

        Task<CaseStageEntity> GetByIdAsync(Guid Id);

        Task<Guid> InsertAsync(CaseStageEntity objEntity);

        Task UpdateAsync(CaseStageEntity objEntity);

        Task DeleteAsync(CaseStageEntity objEntity);
    }
}
