using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Repositories
{
    public interface ICaseWorkMasterRepository
    {
        IQueryable<CaseWorkMasterEntity> QryEntities { get; }

        Task<List<CaseWorkMasterEntity>> GetListAsync();

        Task<CaseWorkMasterEntity> GetByIdAsync(int Id);

        Task<int> InsertAsync(CaseWorkMasterEntity objEntity);

        Task UpdateAsync(CaseWorkMasterEntity objEntity);

        Task DeleteAsync(CaseWorkMasterEntity objEntity);
    }
}
