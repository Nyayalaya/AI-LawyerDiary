using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.CacheRepositories
{
    public interface IDOTypeCacheRepository
    {
        Task<List<DOTypeEntity>> GetCachedListAsync();
        Task<DOTypeEntity> GetByIdAsync(Guid Id);
    }
}
