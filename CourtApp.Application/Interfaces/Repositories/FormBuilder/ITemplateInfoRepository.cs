using CourtApp.Domain.Entities.FormBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Repositories.FormBuilder
{
    public interface ITemplateInfoRepository
    {
        IQueryable<TemplateInfoEntity> Entities { get; }
        Task DeleteAsync(TemplateInfoEntity entity);
        Task<TemplateInfoEntity> GetByIdAsync(Guid Id);
        Task<List<TemplateInfoEntity>> GetListAsync();
        Task<Guid> InsertAsync(TemplateInfoEntity entity);
        Task UpdateAsync(TemplateInfoEntity entity);
    }
}
