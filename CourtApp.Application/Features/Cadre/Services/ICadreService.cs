using CourtApp.Domain.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Cadre.Services
{
    public interface ICadreService
    {
        IQueryable<CadreEntity> Cadres { get; }
        Task<List<CadreEntity>> GetListAsync();
        Task<CadreEntity> GetByIdAsync(Guid cadreId);
        Task<Guid> InsertAsync(CadreEntity cadre);
        Task UpdateAsync(CadreEntity cadre);
        Task DeleteAsync(CadreEntity cadre);
        Task<bool> IsCadreExistAsync(string name);
    }
}
