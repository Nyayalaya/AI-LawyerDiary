using CourtApp.Application.Common;
using CourtApp.Application.DTOs.DropdownDtos;
using CourtApp.Application.Features.CaseCategory.Query;
using CourtApp.Application.Features.CaseCategory.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseCategory.Handlers
{
    public class GetCaseCategoryLookupQueryHandler : IRequestHandler<GetCaseCategoryLookupQuery, Result<List<DdlGuidStringDto>>>
    {
        private readonly ICaseCategoryRepository _repository;
        public GetCaseCategoryLookupQueryHandler(ICaseCategoryRepository _repository)
        {
         this._repository = _repository;
        }
        public async Task<Result<List<DdlGuidStringDto>>> Handle(GetCaseCategoryLookupQuery request, CancellationToken cancellationToken)
        {
            var caseCategories = await _repository
                            .CaseNatures
                            .AsNoTracking()
                            .Where(c => c.CourtTypeId == request.CourtTypeId)
                            .Select(s => new DdlGuidStringDto
                            {
                                Id = s.Id,
                                Name = s.Name
                            })
                            .ToListAsync(cancellationToken);

            return Result<List<DdlGuidStringDto>>.Success(caseCategories);
        }
    }
}
