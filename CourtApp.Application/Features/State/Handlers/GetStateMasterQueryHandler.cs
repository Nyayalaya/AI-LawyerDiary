using CourtApp.Application.Common;
using CourtApp.Application.Extensions;
using CourtApp.Application.Features.State.Query;
using CourtApp.Application.Features.State.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.State.Handlers
{
    public class GetStateMasterQueryHandler : IRequestHandler<GetStateMasterQuery, PaginatedResult<GetStateMasterResponse>>
    {
        private readonly IStateCacheRepository cacheRepository;
        public GetStateMasterQueryHandler(IStateCacheRepository _repositoryCache)
        {
            cacheRepository = _repositoryCache;
        }
        public async Task<PaginatedResult<GetStateMasterResponse>> Handle(GetStateMasterQuery request, CancellationToken cancellationToken)
        {
            var allCourtTypes = await cacheRepository.GetCachedMappedListAsync(cancellationToken);
            var result = allCourtTypes.ToPaginatedResult(request.PageNumber, request.PageSize);
            return result;
        }
    }
}
