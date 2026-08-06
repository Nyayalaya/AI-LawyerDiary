using CourtApp.Application.Common;
using CourtApp.Application.Features.CourtHall.DTOs;
using MediatR;
using System;
using System.Collections.Generic;

namespace CourtApp.Application.Features.CourtHall.Query
{
    public class GetCourtHallCacheQuery : IRequest<Result<List<CourtHallResponse>>>
    {
        public Guid CourtComplexId { get; set; }
    }
}
