using CourtApp.Application.Common;
using CourtApp.Application.Features.CaseStage.Query;
using CourtApp.Domain.Entities.Masters;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseStage.Services
{
    public interface ICaseStageCacheRepository
    {
        //Task<List<CaseStageEntity>> GetCachedListAsync();
        //Task<CaseStageEntity> GetByIdAsync(Guid Id);

        Task<List<CaseStageEntity>> GetCachedListAsync(CancellationToken cancellationToken = default);
        Task<List<CaseStageResponse>> GetCachedMappedListAsync(CancellationToken cancellationToken = default);
        Task<List<Dropdown>> GetDropdownAsync(CancellationToken cancellationToken = default);
    }
}
