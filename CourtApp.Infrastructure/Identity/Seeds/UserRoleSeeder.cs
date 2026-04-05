using CourtApp.Domain.Enums;
using CourtApp.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;

using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Identity.Seeds
{
    public static class UserRoleSeeder
    {
        public static async Task SeedAsync(
            UserManager<ApplicationUser> userManager)
        {
            await AssignRole(userManager, "superadmin@gmail.com", RegisterType.SuperAdmin.ToString());
            await AssignRole(userManager, "lawyer@gmail.com", RegisterType.Lawyer.ToString());
            await AssignRole(userManager, "corporate@gmail.com", RegisterType.Corporate.ToString());
            await AssignRole(userManager, "associate@gmail.com", RegisterType.Associate.ToString());
            await AssignRole(userManager, "clerk@gmail.com", RegisterType.Clerk.ToString());
            await AssignRole(userManager, "client@gmail.com", RegisterType.Client.ToString());
        }

        private static async Task AssignRole(
            UserManager<ApplicationUser> userManager,
            string email,
            string role)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user != null && !await userManager.IsInRoleAsync(user, role))
            {
                await userManager.AddToRoleAsync(user, role);
            }
        }
    }

}
