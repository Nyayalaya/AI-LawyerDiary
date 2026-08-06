using CourtApp.Domain.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Cadre.Services
{
    public interface ICadreMasterRepository
    {
        IQueryable<CadreEntity> Entities { get; }

        Task<List<CadreEntity>> GetListAsync();

        Task<CadreEntity> GetByIdAsync(Guid id);

        Task<Guid> InsertAsync(CadreEntity entity);

        Task UpdateAsync(CadreEntity entity);

        Task DeleteAsync(CadreEntity entity);
    }
}
