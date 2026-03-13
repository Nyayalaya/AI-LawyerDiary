using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Repositories
{
    public interface IDOTypeRepository
    {
        IQueryable<DOTypeEntity> Entities { get; }
        Task<List<DOTypeEntity>> GetListAsync();
        Task<DOTypeEntity> GetByIdAsync(Guid Id);
        Task<Guid> InsertAsync(DOTypeEntity workMasterEntity);
        Task UpdateAsync(DOTypeEntity workMasterEntity);
        Task DeleteAsync(DOTypeEntity workMasterEntity);
    }
}
