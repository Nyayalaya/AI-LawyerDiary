using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseType.Services
{
    public interface ICaseTypeRepository
    {
        IQueryable<TypeOfCasesEntity> QryEntities { get; }

        Task<List<TypeOfCasesEntity>> GetListAsync();

        Task<TypeOfCasesEntity> GetByIdAsync(Guid Id);

        Task<Guid> InsertAsync(TypeOfCasesEntity objEntity);

        Task UpdateAsync(TypeOfCasesEntity objEntity);

        Task DeleteAsync(TypeOfCasesEntity objEntity);
    }
}
