using CourtApp.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;

namespace CourtApp.Application.Features.Court
{
    public class CreateCourtCommand : IRequest<Result<Guid>>
    {
        public Guid LocationId { get; set; }
        public List<CourtDetail> Courts { get; set; }

        public class CourtDetail
        {
            public string Name { get; set; }
            public Guid CourtTypeId { get; set; }
            public Guid CourtLevelId { get; set; }
        }
    }
}
