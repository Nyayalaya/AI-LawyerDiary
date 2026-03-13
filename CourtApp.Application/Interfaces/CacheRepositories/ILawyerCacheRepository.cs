using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.CacheRepositories
{
    public interface ILawyerCacheRepository
    {
        Task<List<LawyerMasterEntity>> GetCachedListAsync();        
        Task<LawyerMasterEntity> GetByIdAsync(Guid Id);
    }
}
