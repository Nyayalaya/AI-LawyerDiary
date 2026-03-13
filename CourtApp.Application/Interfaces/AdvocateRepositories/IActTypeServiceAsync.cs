using CourtApp.Domain.Entities.Advocate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Interfaces
{
    public interface IActTypeServiceAsync 
    {
        public bool isActExist(string actName);
        IQueryable<ActTypeEntity> BookMasters { get; }
        Task<List<ActTypeEntity>> GetListAsync();
        Task<ActTypeEntity> GetByIdAsync(Guid bookTypeId);
        Task<Guid> InsertAsync(ActTypeEntity bookMaster);
        Task UpdateAsync(ActTypeEntity bookMaster);
        Task DeleteAsync(ActTypeEntity BookMaster);
    }
}
