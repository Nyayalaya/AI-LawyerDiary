using CourtApp.Application.Common;
using CourtApp.Application.DTOs.DropdownDtos;
using CourtApp.Application.Features.State.Query;
using CourtApp.Application.Features.State.Services;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.State.Handlers
{
    public class GetStateLookupQueryHandler : IRequestHandler<GetStateLookupQuery, Result<List<DdlIntStringDto>>>
    {
        private readonly IStateCacheRepository stateCache;
        public GetStateLookupQueryHandler(IStateCacheRepository _stateCache) {
            stateCache = _stateCache;
        }
        public async Task<Result<List<DdlIntStringDto>>> Handle(GetStateLookupQuery request, CancellationToken cancellationToken)
        {
            var states=await stateCache.GetCachedMappedListAsync(cancellationToken);
            var result = states.Select(s => new DdlIntStringDto { Id = s.Id, Name = s.Name }).ToList();
            return Result<List<DdlIntStringDto>>.Success(result);
        }
    }
}
