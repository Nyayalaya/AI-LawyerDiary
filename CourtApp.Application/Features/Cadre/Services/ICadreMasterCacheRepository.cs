using CourtApp.Domain.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Cadre.Services
{
    public interface ICadreMasterCacheRepository
    {
        Task<List<CadreEntity>> GetCachedListAsync();

        Task<CadreEntity> GetByIdAsync(Guid id);
    }
}
