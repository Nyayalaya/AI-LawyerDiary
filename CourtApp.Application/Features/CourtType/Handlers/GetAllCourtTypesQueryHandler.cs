using CourtApp.Application.Common;
using CourtApp.Application.Extensions;
using CourtApp.Application.Features.CourtType.Query;
using CourtApp.Application.Features.CourtType.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtType.Handlers
{
    public class GetAllCourtTypesQueryHandler : IRequestHandler<GetAllCourtTypesQuery, Result<PaginatedResult<GetCourtTypeResponse>>>
    {
        private readonly ICourtTypeCacheRepository _cacheRepository;
        private readonly ILogger<GetAllCourtTypesQueryHandler> _logger;

        public GetAllCourtTypesQueryHandler(
            ICourtTypeCacheRepository cacheRepository,
            ILogger<GetAllCourtTypesQueryHandler> logger)
        {
            _cacheRepository = cacheRepository;
            _logger = logger;
        }

        public async Task<Result<PaginatedResult<GetCourtTypeResponse>>> Handle(
            GetAllCourtTypesQuery request,
            CancellationToken cancellationToken)
        {
            // 1️⃣ Get all court types from cache
            var allCourtTypes = await _cacheRepository.GetCachedMappedListAsync(cancellationToken);

            // 2️⃣ Apply in-memory pagination using extension method
            var paginatedResult = allCourtTypes.ToPaginatedResult(request.PageNumber, request.PageSize);

            // 3️⃣ Return success with paginated result
            return Result<PaginatedResult<GetCourtTypeResponse>>.Success(paginatedResult);
        }
    }
}
