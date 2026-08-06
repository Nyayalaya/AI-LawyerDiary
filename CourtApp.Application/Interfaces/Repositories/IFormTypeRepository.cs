using CourtApp.Domain.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Repositories
{
    public interface IFormTypeRepository
    {
        IQueryable<FormTypeEntity> Entities { get; }
        Task<FormTypeEntity> GetByIdAsync(Guid id);
        Task<FormTypeEntity> GetByCodeAsync(string code);
        Task<List<FormTypeEntity>> GetListAsync();
        Task<Guid> InsertAsync(FormTypeEntity entity);
        Task UpdateAsync(FormTypeEntity entity);
        Task DeleteAsync(FormTypeEntity entity);
    }
}
