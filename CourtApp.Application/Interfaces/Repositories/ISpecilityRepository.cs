using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Repositories
{
    public interface ISpecilityRepository
    {
        IQueryable<SpecializationEntity> Entities { get; }
        Task<List<SpecializationEntity>> GetListAsync();
        Task<SpecializationEntity> GetByIdAsync(Guid Id);
        Task<Guid> InsertAsync(SpecializationEntity entity);
        Task UpdateAsync(SpecializationEntity entity);
        Task DeleteAsync(SpecializationEntity entity);
    }
}
