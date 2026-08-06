using CourtApp.Application.Common;
using CourtApp.Domain.Entities.Common;
using MediatR;
using System;
using System.Collections.Generic;

namespace CourtApp.Application.Features.CourtDistrict.Commands
{
    public class UpdateCourtDistrictBatchCommand : IRequest<Result<bool>>
    {
        public List<UpdateCourtDistrictItem> UpdateItems { get; set; }
        public List<LangEntity> Languages { get; set; }
    }

    public class UpdateCourtDistrictItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int StateId { get; set; }
    }
}
