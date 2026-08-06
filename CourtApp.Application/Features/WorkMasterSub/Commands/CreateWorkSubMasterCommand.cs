using CourtApp.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;

namespace CourtApp.Application.Features.WorkMasterSub.Commands
{
    public class CreateWorkSubMasterCommand : IRequest<Result<Guid>>
    {
        public Guid WorkId { get; set; }
        public Guid CourtTypeId { get; set; }
        public List<WorkSubMasterItem> Works { get; set; }
    }

    public class WorkSubMasterItem
    {
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public string Abbreviation { get; set; }
    }
}
