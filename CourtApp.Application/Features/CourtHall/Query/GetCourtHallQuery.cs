using CourtApp.Application.Common;
using CourtApp.Application.Features.CourtHall.DTOs;
using MediatR;
using System;

namespace CourtApp.Application.Features.CourtHall.Query
{
    public class GetCourtHallQuery : IRequest<PaginatedResult<CourtHallResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Guid CourtComplexId { get; set; }
    }
}
