using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Repositories
{
    public interface IWorkMasterSubRepository
    {
        IQueryable<WorkMasterSubEntity> Entities { get; }
        Task<List<WorkMasterSubEntity>> GetListAsync();
        Task<WorkMasterSubEntity> GetByIdAsync(Guid Id);
        Task<Guid> InsertAsync(WorkMasterSubEntity workMasterSubEntity);
        Task UpdateAsync(WorkMasterSubEntity workMasterSubEntity);
        Task DeleteAsync(WorkMasterSubEntity workMasterSubEntity);       
    }
}
