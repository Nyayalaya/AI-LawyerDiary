using CourtApp.Domain.Entities.FormBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.CacheRepositories.FormBuilder
{
    public interface ICaseDraftingCacheRepository
    {
        Task<List<DraftingDetailEntity>> GetCachedListAsync();
        Task<DraftingDetailEntity> GetByIdAsync(Guid bookTypeId);
    }
}
