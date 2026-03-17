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
    public class GetAllCourtTypesQueryHandler : IRequestHandler<GetAllCourtTypesQuery,PaginatedResult<GetCourtTypeResponse>>
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

        public async Task<PaginatedResult<GetCourtTypeResponse>> Handle(
            GetAllCourtTypesQuery request,
            CancellationToken cancellationToken)
        {   
            var allCourtTypes = await _cacheRepository.GetCachedMappedListAsync(cancellationToken);
            var result = allCourtTypes.ToPaginatedResult(request.PageNumber, request.PageSize);
            return result;
        }
    }
}
