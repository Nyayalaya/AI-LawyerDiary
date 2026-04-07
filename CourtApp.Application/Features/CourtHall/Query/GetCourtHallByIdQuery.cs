using CourtApp.Application.Common;
using CourtApp.Application.Features.CourtHall.DTOs;
using MediatR;
using System;

namespace CourtApp.Application.Features.CourtHall.Query
{
    public class GetCourtHallByIdQuery : IRequest<Result<CourtHallByIdResponse>>
    {
        public Guid Id { get; set; }
    }
}
