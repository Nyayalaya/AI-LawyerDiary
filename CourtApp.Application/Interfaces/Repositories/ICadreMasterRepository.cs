using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Repositories
{
    public interface ICadreMasterRepository
    {
        IQueryable<CadreMasterEntity> Entities { get; }

        Task<List<CadreMasterEntity>> GetListAsync();

        Task<CadreMasterEntity> GetByIdAsync(Guid id);

        Task<Guid> InsertAsync(CadreMasterEntity entity);

        Task UpdateAsync(CadreMasterEntity entity);

        Task DeleteAsync(CadreMasterEntity entity);
    }
}
