using CourtApp.Application.Common;
using CourtApp.Application.Features.CaseCategory.Dto;
using MediatR;
using System;

namespace CourtApp.Application.Features.CaseCategory.Queries
{
    public class GetQueryCaseCategory : IRequest<Result<PaginatedResult<CaseCategoryResponse>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Guid CourtTypeId { get; set; }
    }
}
