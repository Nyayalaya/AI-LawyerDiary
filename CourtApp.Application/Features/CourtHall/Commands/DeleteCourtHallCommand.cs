using CourtApp.Application.Common;
using MediatR;
using System;

namespace CourtApp.Application.Features.CourtHall.Commands
{
    public class DeleteCourtHallCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
    }
}
