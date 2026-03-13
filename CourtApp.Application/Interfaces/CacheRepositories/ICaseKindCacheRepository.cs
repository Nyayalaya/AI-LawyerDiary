using CourtApp.Domain.Entities.LawyerDiary;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.CacheRepositories
{
    public interface ICaseKindCacheRepository
    {

        Task<List<CaseKindEntity>> GetCachedListAsync();
        Task<CaseKindEntity> GetByIdAsync(Guid Id);
        //IQueryable<CaseKindEntity> CacheQuerableEntity();
      
    }
}
