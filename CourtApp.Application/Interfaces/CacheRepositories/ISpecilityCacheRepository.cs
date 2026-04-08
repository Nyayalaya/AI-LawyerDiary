using CourtApp.Domain.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.CacheRepositories
{
    public interface ISpecilityCacheRepository
    {
        Task<List<Specialization>> GetCachedListAsync();
        Task<Specialization> GetByIdAsync(Guid Id);
    }
}
