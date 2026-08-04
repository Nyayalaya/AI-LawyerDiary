using CourtApp.Application.Common;
using CourtApp.Application.DTOs.DropdownDtos;
using CourtApp.Application.Features.Cadre.Queries;
using CourtApp.Application.Features.Cadre.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Cadre.Handlers
{
    public class GetCadreQueryLookupHandler : IRequestHandler<GetCadreQueryLookup, Result<List<DdlGuidStringDto>>>
    {
        private readonly ICadreMasterCacheRepository cadreMasterCache;
        public GetCadreQueryLookupHandler(ICadreMasterCacheRepository _cadreMasterCache)
        {
            this.cadreMasterCache = _cadreMasterCache;
        }
        public async Task<Result<List<DdlGuidStringDto>>> Handle(GetCadreQueryLookup request, CancellationToken cancellationToken)
        {
            var cadreList = await cadreMasterCache.GetCachedListAsync();
            var result = cadreList.Select(c => new DdlGuidStringDto
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
            return Result<List<DdlGuidStringDto>>.Success(result);
        }
    }
}
