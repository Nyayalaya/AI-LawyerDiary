using CourtApp.Domain.Entities.CaseDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseDetails.Repositories
{
    public interface ICaseRepository
    {
        IQueryable<CaseEntity> Cases { get; }
        Task<CaseEntity> GetByIdAsync(Guid id);
        Task<bool> IsExistsAsync(CaseEntity entity, Guid excludeId);
        Task AddAsync(CaseEntity entity);
        Task UpdateAsync(CaseEntity entity);
        Task DeleteAsync(CaseEntity entity);
        Task<CaseEntity> GetForUpdateAsync(Guid id);

        Task<CaseEntity> GetCaseHistoryAsync(Guid id,CancellationToken cancellationToken);
    }
}
