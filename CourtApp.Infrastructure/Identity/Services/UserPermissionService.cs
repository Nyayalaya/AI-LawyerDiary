using CourtApp.Application.Features.Permission.Services;
using CourtApp.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Identity.Services
{
    public class UserPermissionService : IUserPermissionService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserPermissionService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool?> HasUserPermissionAsync(string userId, string permission)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return null;

            var claims = await _userManager.GetClaimsAsync(user);

            // ❌ DENY
            if (claims.Any(c =>
                c.Type == "permission" &&
                c.Value == $"DENY:{permission}"))
            {
                return false;
            }

            // ✅ ALLOW
            if (claims.Any(c =>
                c.Type == "permission" &&
                c.Value == permission))
            {
                return true;
            }

            return null;
        }
    }

}
