using CourtApp.Application.Features.Permission.Services;
using CourtApp.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Repositories
{
    public class RolePermissionService : IRolePermissionService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolePermissionService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<bool> HasRolePermissionAsync(string userId, string permission)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return false;

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var roleName in roles)
            {
                var role = await _roleManager.FindByNameAsync(roleName);

                if (role == null) continue;

                var claims = await _roleManager.GetClaimsAsync(role);

                if (claims.Any(c => c.Type == "permission" && c.Value == permission))
                    return true;
            }

            return false;
        }
    }
}
