using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Repositories
{
    public interface IProceedingHeadRepository
    {
        IQueryable<ProceedingHeadEntity> Entities { get; }
        Task<List<ProceedingHeadEntity>> GetListAsync();
        Task<ProceedingHeadEntity> GetByIdAsync(Guid Id);
        Task<Guid> InsertAsync(ProceedingHeadEntity proceedingHeadEntity);
        Task UpdateAsync(ProceedingHeadEntity proceedingHeadEntity);
        Task DeleteAsync(ProceedingHeadEntity proceedingHeadEntity);
    }
}
