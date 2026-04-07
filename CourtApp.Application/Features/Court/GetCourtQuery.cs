using CourtApp.Application.Common;
using CourtApp.Application.Features.Court.DTOs;
using MediatR;
using System;

namespace CourtApp.Application.Features.Court
{
    public class GetCourtQuery : IRequest<Result<PaginatedResult<CourtResponse>>>
    {
        public Guid LocationId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
