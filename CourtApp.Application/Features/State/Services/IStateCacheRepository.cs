using CourtApp.Application.Common;
using CourtApp.Application.Features.State.Query;
using CourtApp.Domain.Entities;
using CourtApp.Entities.Common;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.State.Services
{
    public interface IStateCacheRepository
    {
        Task<List<StateEntity>> GetCachedListAsync(CancellationToken cancellationToken = default);
        Task<List<GetStateMasterResponse>> GetCachedMappedListAsync(CancellationToken cancellationToken = default);
        Task<List<DropdownInt>> GetDropdownAsync(CancellationToken cancellationToken = default);
    }
}
