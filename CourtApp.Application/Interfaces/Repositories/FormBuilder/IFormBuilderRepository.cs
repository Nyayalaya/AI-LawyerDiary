using CourtApp.Domain.Entities.FormBuilder;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace CourtApp.Application.Interfaces.Repositories.FormBuilder
{
    public interface IFormBuilderRepository
    {
        IQueryable<FormBuilderEntity> Entities { get; }
        Task DeleteAsync(FormBuilderEntity entity);
        Task<FormBuilderEntity> GetByIdAsync(Guid Id);
        Task<List<FormBuilderEntity>> GetListAsync();
        Task<Guid> InsertAsync(FormBuilderEntity entity);
        Task UpdateAsync(FormBuilderEntity entity);
    }
}
