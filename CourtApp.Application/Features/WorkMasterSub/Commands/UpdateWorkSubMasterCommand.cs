using CourtApp.Application.Common;
using MediatR;
using System;

namespace CourtApp.Application.Features.WorkMasterSub.Commands
{
    public class UpdateWorkSubMasterCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public Guid WorkId { get; set; }
        public Guid CourtTypeId { get; set; }
        public required string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public string Abbreviation { get; set; }
    }
}
