using CourtApp.Application.Common;
using MediatR;
using System.Collections.Generic;

namespace CourtApp.Application.Features.RoleManager.Queries
{
    public class GetRolePermissionsQuery : IRequest<Result<List<RolePermissionResponse>>>
    {
        public string RoleId { get; set; }
    }

    public class RolePermissionResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
