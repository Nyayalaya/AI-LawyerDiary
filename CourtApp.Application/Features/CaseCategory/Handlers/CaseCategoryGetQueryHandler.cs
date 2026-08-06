using CourtApp.Application.Common;
using CourtApp.Application.Extensions;
using CourtApp.Application.Features.CaseCategory.Dto;
using CourtApp.Application.Features.CaseCategory.Queries;
using CourtApp.Application.Features.CaseCategory.Services;
using CourtApp.Domain.Entities.Masters;
using KT3Core.Areas.Global.Classes;
using MediatR;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseCategory.Handlers
{
    public class CaseCategoryGetQueryHandler : IRequestHandler<GetQueryCaseCategory, Result<PaginatedResult<CaseCategoryResponse>>>
    {
        private readonly ICaseCategoryRepository _repository;
        public CaseCategoryGetQueryHandler(ICaseCategoryRepository repository)
        {
            this._repository = repository;
        }
        public async Task<Result<PaginatedResult<CaseCategoryResponse>>> Handle(GetQueryCaseCategory request, CancellationToken cancellationToken)
        {
            Expression<Func<CaseCategoryEntity, CaseCategoryResponse>> expression = e => new CaseCategoryResponse
            {
                Id = e.Id,
                CourtType = e.CourtType.Name,
                Name = e.Name.ToUpper()
                
            };
            var predicate = PredicateBuilder.True<CaseCategoryEntity>();
            if (request.CourtTypeId != Guid.Empty)
                predicate = predicate.And(b => b.CourtTypeId == request.CourtTypeId);

            var paginatedList = await _repository.CaseNatures.Where(predicate)
                .Select(expression)
                .OrderBy(o => o.Name.ToUpper())
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return await Result<PaginatedResult<CaseCategoryResponse>>.SuccessAsync(paginatedList);
        }
    }
}
