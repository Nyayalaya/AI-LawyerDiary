using CourtApp.Application.Common;
using CourtApp.Application.Features.Court.DTOs;
using MediatR;
using System;
using System.Collections.Generic;

namespace CourtApp.Application.Features.Court.Queries
{
    public class GetCourtCacheQuery : IRequest<Result<List<CourtResponse>>>
    {
        public Guid LocationId { get; set; }
    }
}
