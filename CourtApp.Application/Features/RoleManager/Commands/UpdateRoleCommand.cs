using CourtApp.Application.Common;
using MediatR;
using System;

namespace CourtApp.Application.Features.RoleManager.Commands
{
    public class UpdateRoleCommand : IRequest<Result<bool>>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
