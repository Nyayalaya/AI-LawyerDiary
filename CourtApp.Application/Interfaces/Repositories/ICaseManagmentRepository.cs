using CourtApp.Domain.Entities.CaseDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Repositories
{
    public interface ICaseManagmentRepository
    {
        IQueryable<CaseDetailEntity> Entities { get; }
        Task<List<CaseDetailEntity>> GetListAsync();
        Task<CaseDetailEntity> GetByIdAsync(Guid Id);
        //Task<Guid> InsertAsync(CaseDetailEntity workMasterEntity);
        Task UpdateAsync(CaseDetailEntity workMasterEntity);
        Task DeleteAsync(CaseDetailEntity workMasterEntity);
    }
}
