using CourtApp.Application.Common;
using MediatR;
using System;

namespace CourtApp.Application.Features.WorkMasterSub.Commands
{
    public class DeleteWorkSubMasterCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
    }
}
