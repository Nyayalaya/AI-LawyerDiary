using CourtApp.Application.Features.Permission.Services;
using CourtApp.Application.Features.Profile.Services;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Identity.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IUserPermissionService _userPermissionService;
        private readonly IRolePermissionService _rolePermissionService;
        private readonly IUserHierarchyService _hierarchyService;

        public PermissionService(
            IUserPermissionService userPermissionService,
            IRolePermissionService rolePermissionService,
            IUserHierarchyService hierarchyService)
        {
            _userPermissionService = userPermissionService;
            _rolePermissionService = rolePermissionService;
            _hierarchyService = hierarchyService;
        }

        public async Task<bool> HasPermissionAsync(string userId, string permission)
        {
            // =============================
            // 1. USER OVERRIDE
            // =============================
            var userOverride = await _userPermissionService
                .HasUserPermissionAsync(userId, permission);

            if (userOverride.HasValue)
                return userOverride.Value;

            // =============================
            // 2. ROLE PERMISSION
            // =============================
            var hasRolePermission = await _rolePermissionService
                .HasRolePermissionAsync(userId, permission);

            if (hasRolePermission)
                return true;

            // =============================
            // 3. HIERARCHY INHERITANCE 🔥
            // =============================
            var parentIds = await _hierarchyService
                .GetAllParentIdsAsync(userId);

            foreach (var parentId in parentIds)
            {
                var hasParentPermission = await HasPermissionAsync(parentId, permission);

                if (hasParentPermission)
                    return true;
            }

            return false;
        }
    }

}
