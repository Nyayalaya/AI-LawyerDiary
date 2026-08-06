using CourtApp.Domain.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Repositories
{
    public interface ISpecilityRepository
    {
        IQueryable<Specialization> Entities { get; }
        Task<List<Specialization>> GetListAsync();
        Task<Specialization> GetByIdAsync(Guid Id);
        Task<Guid> InsertAsync(Specialization entity);
        Task UpdateAsync(Specialization entity);
        Task DeleteAsync(Specialization entity);
    }
}
