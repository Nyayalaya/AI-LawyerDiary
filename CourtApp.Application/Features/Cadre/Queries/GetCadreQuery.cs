using CourtApp.Application.Common;
using CourtApp.Application.Features.Cadre.Dtos;
using MediatR;

namespace CourtApp.Application.Features.Cadre.Queries
{
    public class GetCadreQuery : IRequest<PaginatedResult<CadreResponse>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SearchTerm { get; set; }
    }
}
