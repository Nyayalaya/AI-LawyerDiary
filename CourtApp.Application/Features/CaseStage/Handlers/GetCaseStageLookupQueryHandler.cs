using CourtApp.Application.Common;
using CourtApp.Application.DTOs.DropdownDtos;
using CourtApp.Application.Features.CaseStage.Query;
using CourtApp.Application.Features.CaseStage.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseStage.Handlers
{
    public class GetCaseStageLookupQueryHandler : IRequestHandler<GetCaseStageLookupQuery, Result<List<DdlGuidStringDto>>>
    {
        private readonly ICaseStageCacheRepository _cacheRepo;
        public GetCaseStageLookupQueryHandler(ICaseStageCacheRepository _cacheRepo)
        {
            this._cacheRepo = _cacheRepo;
        }
        public async Task<Result<List<DdlGuidStringDto>>> Handle(GetCaseStageLookupQuery request, CancellationToken cancellationToken)
        {
            var caseStages = await _cacheRepo.GetCachedListAsync();
            var result=caseStages.ConvertAll(x => new DdlGuidStringDto { Id = x.Id, Name = x.Name });
            return Result<List<DdlGuidStringDto>>.Success(result);
        }
    }
}
