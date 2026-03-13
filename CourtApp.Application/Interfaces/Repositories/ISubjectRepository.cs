using CourtApp.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Repositories
{
    public interface ISubjectRepository
    {
        IQueryable<SubjectEntity> Subjects { get; }

        Task<List<SubjectEntity>> GetListAsync();

        Task<SubjectEntity> GetByIdAsync(Guid Id);

        Task<Guid> InsertAsync(SubjectEntity subject);

        Task UpdateAsync(SubjectEntity subject);

        Task DeleteAsync(SubjectEntity subject);
    }
}
