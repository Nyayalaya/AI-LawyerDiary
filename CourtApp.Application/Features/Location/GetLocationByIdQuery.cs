using CourtApp.Application.Common;
using CourtApp.Application.DTOs.Location;
using MediatR;
using System;

namespace CourtApp.Application.Features.Location
{
    public class GetLocationByIdQuery : IRequest<Result<LocationByIdResponse>>
    {
        public Guid Id { get; set; }
    }
}
