using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Repositories
{
    public interface ICourtComplexRepository
    {
        IQueryable<CourtComplexEntity> Entities { get; }
        Task<List<CourtComplexEntity>> GetListAsync();
        Task<CourtComplexEntity> GetByIdAsync(Guid Id);
        Task<Guid> InsertAsync(CourtComplexEntity Entity);
        Task UpdateAsync(CourtComplexEntity Entity);
        Task DeleteAsync(CourtComplexEntity Entity);
    }
}
