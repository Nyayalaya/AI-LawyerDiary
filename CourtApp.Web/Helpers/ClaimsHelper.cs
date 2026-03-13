using CourtApp.Application.Constants;
using CourtApp.Infrastructure.Identity.Models;
using CourtApp.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CourtApp.Web.Helpers
{
    public static class ClaimsHelper
    {
        public static void HasRequiredClaims(this ClaimsPrincipal claimsPrincipal, IEnumerable<string> permissions)
        {
            if (!claimsPrincipal.Identity.IsAuthenticated)
            {
                return;
            }
            var allClaims = claimsPrincipal.Claims.Select(a => a.Value).ToList();
            var success = allClaims.Intersect(permissions).Any();
            if (!success)
            {
                throw new Exception();
            }
            return;
        }

        public static void GetPermissions(this List<RoleClaimsViewModel> allPermissions, Type policy, string roleId)
        {
            FieldInfo[] fields = policy.GetFields(BindingFlags.Static | BindingFlags.Public);

            foreach (FieldInfo fi in fields)
            {
                allPermissions.Add(new RoleClaimsViewModel { Value = fi.GetValue(null).ToString(), Type = "Permissions" });
            }
        }

        public static async Task AddPermissionClaim(this RoleManager<IdentityRole> roleManager, IdentityRole role, string permission)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission))
            {
                await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, permission));
            }
        }

        public static async Task AddPermissionToOperator(this UserManager<ApplicationUser> userManager, ApplicationUser operatorUser, string permission)
        {
            var userClaims = await userManager.GetClaimsAsync(operatorUser);
            if (!userClaims.Any(c => c.Type == "Permission" && c.Value == permission))
            {
                await userManager.AddClaimAsync(operatorUser, new Claim("Permission", permission));
            }
        }
        public static async Task RemovePermissionFromOperator(this UserManager<ApplicationUser> userManager, ApplicationUser operatorUser, string permission)
        {
            var userClaims = await userManager.GetClaimsAsync(operatorUser);
            var claimToRemove = userClaims.FirstOrDefault(c => c.Type == "Permission" && c.Value == permission);
            if (claimToRemove != null)
            {
                await userManager.RemoveClaimAsync(operatorUser, claimToRemove);
            }
        }
    }
}