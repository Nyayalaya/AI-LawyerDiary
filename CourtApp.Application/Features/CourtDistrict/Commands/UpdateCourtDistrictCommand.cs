using CourtApp.Application.Common;
using CourtApp.Domain.Entities.Common;
using MediatR;
using System;
using System.Collections.Generic;

namespace CourtApp.Application.Features.CourtDistrict.Commands
{
    public class UpdateCourtDistrictCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int StateId { get; set; }
        public List<LangEntity> Languages { get; set; }
    }
}
