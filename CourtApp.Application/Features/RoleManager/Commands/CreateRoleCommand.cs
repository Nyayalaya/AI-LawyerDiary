using CourtApp.Application.Common;
using MediatR;
using System;

namespace CourtApp.Application.Features.RoleManager.Commands
{
    public class CreateRoleCommand : IRequest<Result<Guid>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
