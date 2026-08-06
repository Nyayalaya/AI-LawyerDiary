using CourtApp.Application.Common;
using CourtApp.Application.Extensions;
using CourtApp.Application.Features.CourtLevel.Query;
using CourtApp.Application.Features.CourtLevel.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtLevel.Handlers
{
    public class GetCourtLevelQueryHandler : IRequestHandler<GetCourtLevelQuery, PaginatedResult<GetCourtLevelResponse>>
    {
        private readonly ICourtLevelCacheRepository cacheRepository;
        public GetCourtLevelQueryHandler(ICourtLevelCacheRepository _repositoryCache)
        {
            cacheRepository = _repositoryCache;
        }
        public async Task<PaginatedResult<GetCourtLevelResponse>> Handle(GetCourtLevelQuery request, CancellationToken cancellationToken)
        {
            var allCourtLevels = await cacheRepository.GetCachedMappedListAsync(cancellationToken);
            var result = allCourtLevels.ToPaginatedResult(request.PageNumber, request.PageSize);
            return result;
        }
    }
}
