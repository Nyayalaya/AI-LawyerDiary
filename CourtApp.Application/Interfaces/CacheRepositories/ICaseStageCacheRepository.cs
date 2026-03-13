using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.CacheRepositories
{
    public interface ICaseStageCacheRepository
    {
        Task<List<CaseStageEntity>> GetCachedListAsync();
        Task<CaseStageEntity> GetByIdAsync(Guid Id);
    }
}
