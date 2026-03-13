using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Repositories
{
    public interface ILawyerRepository
    {
        IQueryable<LawyerMasterEntity> Entities { get; }

        Task<List<LawyerMasterEntity>> GetListAsync();

        Task<LawyerMasterEntity> GetByIdAsync(Guid Id);

        Task<Guid> InsertAsync(LawyerMasterEntity lawyer);

        Task UpdateAsync(LawyerMasterEntity lawyer);

        Task DeleteAsync(LawyerMasterEntity lawyer);
    }
}
