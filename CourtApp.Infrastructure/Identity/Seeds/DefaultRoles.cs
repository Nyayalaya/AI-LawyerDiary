using CourtApp.Application.Enums;
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
            await roleManager.CreateAsync(new IdentityRole(Roles.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Lawyer.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Clerk.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Associate.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Corporate.ToString()));
        }
    }
}