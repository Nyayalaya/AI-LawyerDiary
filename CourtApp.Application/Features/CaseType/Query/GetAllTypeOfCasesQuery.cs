using CourtApp.Application.Common;
using CourtApp.Application.Features.Typeofcasess.Query;
using MediatR;
using System;

namespace CourtApp.Application.Features.TypeOfCases.Query
{
    public class GetAllTypeOfCasesQuery : IRequest<Result<PaginatedResult<GetAllTypeOfCasesResponse>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Guid CategoryId { get; set; }
        public Guid CourtTypeId { get; set; }
        public GetAllTypeOfCasesQuery(int pagenumber, int pagesize)
        {
            PageNumber = pagenumber;
            PageSize = pagesize;
        }
    }

    
}
