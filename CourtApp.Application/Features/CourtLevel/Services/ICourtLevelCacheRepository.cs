using CourtApp.Application.Common;
using CourtApp.Application.Features.CourtLevel.Query;
using CourtApp.Domain.Entities.Masters;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtLevel.Services
{
    public interface ICourtLevelCacheRepository
    {
        Task<List<CourtLevelEntity>> GetCachedListAsync(CancellationToken cancellationToken = default);
        Task<List<GetCourtLevelResponse>> GetCachedMappedListAsync(CancellationToken cancellationToken = default);
        Task<List<Dropdown>> GetDropdownAsync(CancellationToken cancellationToken = default);
    }
}
