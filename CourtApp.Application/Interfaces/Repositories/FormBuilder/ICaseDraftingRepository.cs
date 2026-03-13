using CourtApp.Domain.Entities.FormBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Repositories.FormBuilder
{
    public interface ICaseDraftingRepository
    {
        IQueryable<DraftingDetailEntity> Entities { get; }
        Task DeleteAsync(DraftingDetailEntity entity);
        Task<DraftingDetailEntity> GetByIdAsync(Guid Id);
        Task<List<DraftingDetailEntity>> GetListAsync();
        Task<Guid> InsertAsync(DraftingDetailEntity entity);
        Task UpdateAsync(DraftingDetailEntity entity);
    }
}
