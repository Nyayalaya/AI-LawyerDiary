using CourtApp.Domain.Entities.FormBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Repositories.FormBuilder
{
    public interface IFormTempMappingRepository
    {
        IQueryable<FormTemplateMappingEntity> Entities { get; }
        Task DeleteAsync(FormTemplateMappingEntity entity);
        Task<FormTemplateMappingEntity> GetByIdAsync(Guid Id);
        Task<List<FormTemplateMappingEntity>> GetListAsync();
        Task<Guid> InsertAsync(FormTemplateMappingEntity entity);
        Task UpdateAsync(FormTemplateMappingEntity entity);
    }
}
