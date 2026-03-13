using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Repositories
{
    public interface ICourtMasterRepository
    {
        IQueryable<CourtMasterEntity> QryEntities { get; }
        IQueryable<CourtMasterEntity> Entities { get; }

        Task<List<CourtMasterEntity>> GetListAsync();

        Task<CourtMasterEntity> GetByIdAsync(Guid Id);

        Task<Guid> InsertAsync(CourtMasterEntity objEntity);

        Task UpdateAsync(CourtMasterEntity objEntity);

        Task DeleteAsync(CourtMasterEntity objEntity);
    }
}
