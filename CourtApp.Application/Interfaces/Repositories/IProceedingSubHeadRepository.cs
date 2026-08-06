using CourtApp.Domain.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Repositories
{
    public interface IProceedingSubHeadRepository
    {
        IQueryable<ProceedingEntity> Entities { get; }
        Task<List<ProceedingEntity>> GetListAsync();
        Task<ProceedingEntity> GetByIdAsync(Guid Id);
        Task<Guid> InsertAsync(ProceedingEntity proceedingSubHeadEntity);
        Task UpdateAsync(ProceedingEntity proceedingSubHeadEntity);
        Task DeleteAsync(ProceedingEntity proceedingSubHeadEntity);
    }
}
