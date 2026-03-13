using CourtApp.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.CacheRepositories
{
    public interface ISubjectCacheRepository
    {
        Task<List<SubjectEntity>> GetCachedListAsync();

        Task<SubjectEntity> GetByIdAsync(Guid Id);
    }
}
