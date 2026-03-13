using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseType.Services
{
    public interface ICaseTypeCacheRepository
    {
        Task<List<TypeOfCasesEntity>> GetCachedListAsync();

        Task<TypeOfCasesEntity> GetByIdAsync(Guid Id);
    }
}
