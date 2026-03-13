using CourtApp.Domain.Entities.LawyerDiary;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace CourtApp.Application.Interfaces.CacheRepositories
{
    public interface IFSTitleCacheRepository
    {
        Task<List<FSTitleEntity>> GetCachedListAsync();
        Task<FSTitleEntity> GetCachedByIdAsync(Guid Id);
    }
}
