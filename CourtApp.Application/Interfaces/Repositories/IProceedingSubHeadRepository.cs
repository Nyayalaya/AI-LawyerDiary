using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Repositories
{
    public interface IProceedingSubHeadRepository
    {
        IQueryable<ProceedingSubHeadEntity> Entities { get; }
        Task<List<ProceedingSubHeadEntity>> GetListAsync();
        Task<ProceedingSubHeadEntity> GetByIdAsync(Guid Id);
        Task<Guid> InsertAsync(ProceedingSubHeadEntity proceedingSubHeadEntity);
        Task UpdateAsync(ProceedingSubHeadEntity proceedingSubHeadEntity);
        Task DeleteAsync(ProceedingSubHeadEntity proceedingSubHeadEntity);
    }
}
