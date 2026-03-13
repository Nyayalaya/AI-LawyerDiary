using AspNetCoreHero.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Queries.Cases
{
    public class QueryGetAllCaseEntry : IRequest<PaginatedResult<ResponseGetAllCaseEntry>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
