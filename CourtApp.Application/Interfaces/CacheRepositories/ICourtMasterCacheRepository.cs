using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.CacheRepositories
{
    public interface ICourtMasterCacheRepository
    {
        Task<List<CourtMasterEntity>> GetCachedListAsync();

        Task<CourtMasterEntity> GetByIdAsync(Guid UniqueId);
    }
}
