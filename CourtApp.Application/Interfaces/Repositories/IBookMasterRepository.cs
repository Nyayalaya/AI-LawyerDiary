using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Repositories
{
    public interface IBookMasterRepository
    {
        IQueryable<LDBookEntity> BookMasters { get; }

        Task<List<LDBookEntity>> GetListAsync();

        Task<LDBookEntity> GetByIdAsync(Guid bookTypeId);

        Task<Guid> InsertAsync(LDBookEntity bookMaster);

        Task UpdateAsync(LDBookEntity bookMaster);

        Task DeleteAsync(LDBookEntity BookMaster);
    }
}
