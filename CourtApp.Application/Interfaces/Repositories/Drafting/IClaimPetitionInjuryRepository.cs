using CourtApp.Domain.Entities.FormBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Repositories.Drafting
{
    public interface IDynamicFormBuilderRepository
    {
        IQueryable<FormBuilderEntity> Entities { get; }
        Task DeleteAsync(FormBuilderEntity entity);
        Task<FormBuilderEntity> GetByIdAsync(Guid Id);
        Task<List<FormBuilderEntity>> GetListAsync();
        Task<Guid> InsertAsync(FormBuilderEntity entity);
        Task UpdateAsync(FormBuilderEntity entity);
    }
}
