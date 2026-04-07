using CourtApp.Application.Common;
using MediatR;
using System;

namespace CourtApp.Application.Features.WorkMaster.Commands
{
    public class DeleteWorkMasterCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
    }
}
