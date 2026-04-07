using CourtApp.Domain.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Cadre.Services
{
    public interface ICadreService
    {
        IQueryable<CadreMasterEntity> Cadres { get; }
        Task<List<CadreMasterEntity>> GetListAsync();
        Task<CadreMasterEntity> GetByIdAsync(Guid cadreId);
        Task<Guid> InsertAsync(CadreMasterEntity cadre);
        Task UpdateAsync(CadreMasterEntity cadre);
        Task DeleteAsync(CadreMasterEntity cadre);
        Task<bool> IsCadreExistAsync(string name);
    }
}
