using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using CourtApp.Domain.Entities.FormBuilder;

namespace CourtApp.Application.Interfaces.CacheRepositories.FormBuilder
{
    public interface IFormBuilderCacheRepository
    {
        Task<List<FormBuilderEntity>> GetCachedListAsync();
        Task<FormBuilderEntity> GetByIdAsync(Guid bookTypeId);
    }
}
