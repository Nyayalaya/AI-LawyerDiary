using CourtApp.Domain.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Repositories
{
    public interface IWorkMasterSubRepository
    {
        IQueryable<WorksEntity> Entities { get; }
        Task<List<WorksEntity>> GetListAsync();
        Task<WorksEntity> GetByIdAsync(Guid Id);
        Task<Guid> InsertAsync(WorksEntity workMasterSubEntity);
        Task UpdateAsync(WorksEntity workMasterSubEntity);
        Task DeleteAsync(WorksEntity workMasterSubEntity);       
    }
}
