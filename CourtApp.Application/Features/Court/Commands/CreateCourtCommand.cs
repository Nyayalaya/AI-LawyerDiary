using CourtApp.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;

namespace CourtApp.Application.Features.Court.Commands
{
    public class CreateCourtCommand : IRequest<Result<string>>
    {
        public Guid CourtTypeId { get; set; }
        public int StateId { get; set; }
        public bool IsVirtualCourt { get; set; }
        public List<CourtDetail> Courts { get; set; }

        public class CourtDetail
        {
            public string Name { get; set; }
            public string Code { get; set; }
            public Guid? CourtDistrictId { get; set; }
            public Guid CourtLevelId { get; set; }
        }
    }
}
