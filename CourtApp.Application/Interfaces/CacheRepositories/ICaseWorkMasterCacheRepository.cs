using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.CacheRepositories
{
    public interface ICaseWorkMasterCacheRepository
    {
        IQueryable<CaseWorkMasterEntity> QryEntities { get; }

        //Task<List<CaseWorkMasterEntity>> GetListAsync();
        Task<CaseWorkMasterEntity> GetByIdAsync(int Id);

        
    }
}
