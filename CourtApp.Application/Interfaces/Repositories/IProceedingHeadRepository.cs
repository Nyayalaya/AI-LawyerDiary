using CourtApp.Domain.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Repositories
{
    public interface IProceedingHeadRepository
    {
        IQueryable<ProceedingTypeEntity> Entities { get; }
        Task<List<ProceedingTypeEntity>> GetListAsync();
        Task<ProceedingTypeEntity> GetByIdAsync(Guid Id);
        Task<Guid> InsertAsync(ProceedingTypeEntity proceedingHeadEntity);
        Task UpdateAsync(ProceedingTypeEntity proceedingHeadEntity);
        Task DeleteAsync(ProceedingTypeEntity proceedingHeadEntity);
    }
}
