using CourtApp.Domain.Entities.CaseDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.CacheRepositories
{
    public interface ICaseTitleCacheRepository
    {        
        Task<List<CaseTitleEntity>> GetCachedListAsync();
        Task<CaseTitleEntity> GetByIdAsync(Guid CaseId);
    }
}
