using CourtApp.Domain.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseCategory.Services
{
    public interface ICaseCategoryCacheRepository
    {
        Task<List<CaseCategoryEntity>> GetCachedListAsync();
        Task<CaseCategoryEntity> GetByIdAsync(Guid bookTypeId);
        
    }
}
