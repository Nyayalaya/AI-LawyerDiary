using CourtApp.Application.Common;
using CourtApp.Domain.Entities.Common;
using MediatR;
using System;
using System.Collections.Generic;

namespace CourtApp.Application.Features.CourtDistrict
{
    public class CreateCourtDistrictCommand : IRequest<Result<Guid>>
    {
        public int StateId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public List<LangEntity> Languages { get; set; }
    }
}
