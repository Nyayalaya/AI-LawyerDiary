using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.CacheRepositories
{
    public interface IPublicationCacheRepository
    {
        Task<List<PublisherEntity>> GetCachedListAsync();

        Task<PublisherEntity> GetByIdAsync(Guid bookTypeId);
    }
}
