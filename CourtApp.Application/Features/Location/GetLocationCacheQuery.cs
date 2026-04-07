using CourtApp.Application.Common;
using CourtApp.Application.DTOs.Location;
using MediatR;
using System.Collections.Generic;

namespace CourtApp.Application.Features.Location
{
    public class GetLocationCacheQuery : IRequest<Result<List<LocationResponse>>>
    {
        public int StateId { get; set; }
    }
}
