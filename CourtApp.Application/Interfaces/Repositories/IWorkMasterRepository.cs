using CourtApp.Domain.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Repositories
{
    public interface IWorkMasterRepository
    {
        IQueryable<WorkTypeEntity> Entities { get; }
        Task<List<WorkTypeEntity>> GetListAsync();
        Task<WorkTypeEntity> GetByIdAsync(Guid Id);
        Task<Guid> InsertAsync(WorkTypeEntity workMasterEntity);
        Task UpdateAsync(WorkTypeEntity workMasterEntity);
        Task DeleteAsync(WorkTypeEntity workMasterEntity);
    }
}
