using CourtApp.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Identity.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole("SUPERADMIN"));
            //await roleManager.CreateAsync(new IdentityRole(RegisterType.Lawyer.ToString()));
            //await roleManager.CreateAsync(new IdentityRole(RegisterType.Clerk.ToString()));
            //await roleManager.CreateAsync(new IdentityRole(RegisterType.Associate.ToString()));
            //await roleManager.CreateAsync(new IdentityRole(RegisterType.Corporate.ToString()));
        }
    }
}