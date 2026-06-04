using CourtApp.Domain.Entities.CaseDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseDetails.Repositories
{
    public interface ICaseAssignedRepository
    {
        IQueryable<CaseAssignedEntity> Entities { get; }
        Task<Guid> InsertAsync(CaseAssignedEntity entity);
        Task UpdateAsync(CaseAssignedEntity entity);
        Task DeleteRangeAsync(List<CaseAssignedEntity> entity);
    }
}
