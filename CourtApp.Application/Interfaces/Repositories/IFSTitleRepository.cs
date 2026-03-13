using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Repositories
{
    public interface IFSTitleRepository
    {
        IQueryable<FSTitleEntity> Entities { get; }
        Task<List<FSTitleEntity>> GetListAsync();
        Task<FSTitleEntity> GetByIdAsync(Guid Id);
        Task<Guid> InsertAsync(FSTitleEntity entity);
        Task UpdateAsync(FSTitleEntity entity);
        Task DeleteAsync(FSTitleEntity entity);
    }
}
