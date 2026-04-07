using CourtApp.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;

namespace CourtApp.Application.Features.CourtHall.Commands
{
    public class CreateCourtHallCommand : IRequest<Result<Guid>>
    {
        public Guid CourtComplexId { get; set; }
        public List<CourtHallDetail> Halls { get; set; }
    }

    public class CourtHallDetail
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string JudgeName { get; set; }
        public string RoomNumber { get; set; }
    }
}
