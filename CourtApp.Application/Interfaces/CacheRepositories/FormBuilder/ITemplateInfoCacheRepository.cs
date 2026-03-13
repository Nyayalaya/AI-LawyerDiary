using CourtApp.Domain.Entities.FormBuilder;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.CacheRepositories.FormBuilder
{
    public interface ITemplateInfoCacheRepository
    {
        Task<List<TemplateInfoEntity>> GetCachedListAsync();
        Task<TemplateInfoEntity> GetByIdAsync(Guid Id);
    }
}
