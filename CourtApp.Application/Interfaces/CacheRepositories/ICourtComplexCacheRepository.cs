using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.CacheRepositories
{
    public interface ICourtComplexCacheRepository
    {
        Task<List<CourtComplexEntity>> GetCachedListAsync();
        Task<CourtComplexEntity> GetByIdAsync(Guid Id);
    }
}
