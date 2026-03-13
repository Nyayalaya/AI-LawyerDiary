using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Repositories
{
    public interface IWorkMasterRepository
    {
        IQueryable<WorkMasterEntity> Entities { get; }
        Task<List<WorkMasterEntity>> GetListAsync();
        Task<WorkMasterEntity> GetByIdAsync(Guid Id);
        Task<Guid> InsertAsync(WorkMasterEntity workMasterEntity);
        Task UpdateAsync(WorkMasterEntity workMasterEntity);
        Task DeleteAsync(WorkMasterEntity workMasterEntity);
    }
}
