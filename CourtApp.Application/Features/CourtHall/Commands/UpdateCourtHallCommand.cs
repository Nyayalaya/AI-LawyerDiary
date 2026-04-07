using CourtApp.Application.Common;
using MediatR;
using System;

namespace CourtApp.Application.Features.CourtHall.Commands
{
    public class UpdateCourtHallCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public Guid CourtComplexId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string JudgeName { get; set; }
        public string RoomNumber { get; set; }
    }
}
