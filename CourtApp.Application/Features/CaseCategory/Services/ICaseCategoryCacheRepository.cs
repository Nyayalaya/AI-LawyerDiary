using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseCategory.Services
{
    public interface ICaseCategoryCacheRepository
    {
        Task<List<NatureEntity>> GetCachedListAsync();
        Task<NatureEntity> GetByIdAsync(Guid bookTypeId);
        
    }
}
