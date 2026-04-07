using CourtApp.Application.Common;
using CourtApp.Application.Features.Court.DTOs;
using MediatR;
using System;

namespace CourtApp.Application.Features.Court
{
    public class GetCourtByIdQuery : IRequest<Result<CourtByIdResponse>>
    {
        public Guid Id { get; set; }
    }
}
