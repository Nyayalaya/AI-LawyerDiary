using CourtApp.Application.Common;
using MediatR;
using System;

namespace CourtApp.Application.Features.WorkMaster.Commands
{
    public class CreateWorkMasterCommand : IRequest<Result<Guid>>
    {
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public string Abbreviation { get; set; }
        public Guid CourtTypeId { get; set; }
    }
}
