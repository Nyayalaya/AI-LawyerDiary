using CourtApp.Application.Common;
using CourtApp.Application.Extensions;
using CourtApp.Application.Features.CaseStage.Query;
using CourtApp.Application.Features.CaseStage.Services;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseStage.Handlers
{
    public class CaseStageQueryHandler : IRequestHandler<CaseStageQuery, PaginatedResult<CaseStageResponse>>
    {
        private readonly ICaseStageCacheRepository _cacheRep;
        public CaseStageQueryHandler(ICaseStageCacheRepository _cacheRep)
        {
            this._cacheRep = _cacheRep;
        }
        public async Task<PaginatedResult<CaseStageResponse>> Handle(CaseStageQuery request, CancellationToken cancellationToken)
        {
            var data = await _cacheRep.GetCachedMappedListAsync(cancellationToken);
            var result = data.ToPaginatedResult(request.PageNumber, request.PageSize);
            return result;
        }
    }
}
