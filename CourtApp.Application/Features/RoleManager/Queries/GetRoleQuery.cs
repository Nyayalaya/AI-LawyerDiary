using CourtApp.Application.Common;
using MediatR;

namespace CourtApp.Application.Features.RoleManager.Queries
{
    public class GetRoleQuery : IRequest<PaginatedResult<RoleResponse>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SearchTerm { get; set; }
    }

    public class RoleResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PermissionCount { get; set; }
        public int UserCount { get; set; }
    }
}
