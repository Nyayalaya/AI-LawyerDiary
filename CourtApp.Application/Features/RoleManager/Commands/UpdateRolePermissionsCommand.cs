using CourtApp.Application.Common;
using MediatR;
using System.Collections.Generic;
using System;

namespace CourtApp.Application.Features.RoleManager.Commands
{
    public class UpdateRolePermissionsCommand : IRequest<Result<bool>>
    {
        public string RoleId { get; set; }
        public List<RolePermissionItem> Permissions { get; set; }
    }

    public class RolePermissionItem
    {
        public string PermissionId { get; set; }
        public bool IsActive { get; set; }
    }
}
