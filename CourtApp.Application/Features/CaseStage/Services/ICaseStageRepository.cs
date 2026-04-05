using CourtApp.Domain.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseStage.Services
{
    public interface ICaseStageRepository
    {
        IQueryable<CaseStageEntity> Entities { get; }

        Task<List<CaseStageEntity>> GetListAsync();

        Task<CaseStageEntity> GetByIdAsync(Guid Id);

        Task<Guid> InsertAsync(CaseStageEntity objEntity);

        Task UpdateAsync(CaseStageEntity objEntity);

        Task DeleteAsync(CaseStageEntity objEntity);
    }
}
