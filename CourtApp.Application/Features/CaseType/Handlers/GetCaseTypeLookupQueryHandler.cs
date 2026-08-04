using CourtApp.Application.Common;
using CourtApp.Application.DTOs.DropdownDtos;
using CourtApp.Application.Features.CaseType.Query;
using CourtApp.Application.Features.CaseType.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseType.Handlers
{
    public class GetCaseTypeLookupQueryHandler : IRequestHandler<GetCaseTypeLookupQuery, Result<List<DdlGuidStringDto>>>
    {
        private readonly ICaseTypeCacheRepository _repository;
        public GetCaseTypeLookupQueryHandler(ICaseTypeCacheRepository _repository)
        {
            this._repository = _repository;

        }
        public async Task<Result<List<DdlGuidStringDto>>> Handle(GetCaseTypeLookupQuery request, CancellationToken cancellationToken)
        {
            var caseTypes = await _repository.GetCachedListAsync();
            var result = caseTypes.Select(x => new DdlGuidStringDto
            {
                Id = x.Id,
                Name = x.Name_En
            }).ToList();
            return Result<List<DdlGuidStringDto>>.Success(result);
        }
    }
}
