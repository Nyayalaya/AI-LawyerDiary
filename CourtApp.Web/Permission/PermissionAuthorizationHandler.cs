using CourtApp.Application.Constants;
using CourtApp.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CourtApp.Web.Permission
{
    internal class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public PermissionAuthorizationHandler(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User == null)
            {
                return;
            }
            var user = await _userManager.GetUserAsync(context.User);
            var userRoleNames = await _userManager.GetRolesAsync(user);
            var userRoles = _roleManager.Roles.Where(x => userRoleNames.Contains(x.Name)).ToList();
            var roleClaims = new List<Claim>();
            foreach (var role in userRoles)
            {
                var claims = await _roleManager.GetClaimsAsync(role);
                roleClaims.AddRange(claims);
            }
            // Fetch user-specific claims
            var userClaims = await _userManager.GetClaimsAsync(user);
            // Combine role-based and user-specific claims
            var allClaims = roleClaims.Concat(userClaims);
            var permissions = allClaims.Where(x => x.Type == CustomClaimTypes.Permission &&
                                                       x.Value == requirement.Permission &&
                                                       x.Issuer == "LOCAL AUTHORITY")
                                           .Select(x => x.Value);

            if (permissions.Any())
            {
                context.Succeed(requirement);
                return;
            }
            //foreach (var role in userRoles)
            //{
            //    var roleClaims = await _roleManager.GetClaimsAsync(role);

            //    var permissions = roleClaims.Where(x => x.Type == CustomClaimTypes.Permission &&
            //                                            x.Value == requirement.Permission &&
            //                                            x.Issuer == "LOCAL AUTHORITY")
            //                                .Select(x => x.Value);

            //    if (permissions.Any())
            //    {
            //        context.Succeed(requirement);
            //        return;
            //    }
            //}
        }
    }
}