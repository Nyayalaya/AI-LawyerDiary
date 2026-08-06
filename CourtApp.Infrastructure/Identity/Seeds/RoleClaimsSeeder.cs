using CourtApp.Application.Constants;
using CourtApp.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Identity.Seeds
{
    public static class RoleClaimsSeeder
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
        {
            foreach (var roleMap in RolePermissions)
            {
                var roleName = roleMap.Key.ToString();
                var permissions = roleMap.Value;

                var role = await roleManager.FindByNameAsync(roleName);
                if (role == null) continue;

                var existingClaims = await roleManager.GetClaimsAsync(role);

                var existingPermissionValues = existingClaims
                    .Where(c => c.Type == CustomClaimTypes.Permission)
                    .Select(c => c.Value)
                    .ToHashSet();

                var newClaims = permissions
                    .Where(p => !existingPermissionValues.Contains(p))
                    .Select(p => new Claim(CustomClaimTypes.Permission, p));

                foreach (var claim in newClaims)
                {
                    await roleManager.AddClaimAsync(role, claim);
                }
            }
        }

        private static Dictionary<RegisterType, List<string>> RolePermissions =>
            new()
            {
            { RegisterType.SuperAdmin, Permissions.GetAllPermissions() },
            { RegisterType.Lawyer, Permissions.LawyerPermissions() },
            { RegisterType.Corporate, Permissions.CorporatePermissions() },
            { RegisterType.Associate, Permissions.AssociatePermissions() },
            { RegisterType.Clerk, Permissions.ClerkPermissions() },
            { RegisterType.Client, Permissions.ClientPermissions() }
            };
    }
}
