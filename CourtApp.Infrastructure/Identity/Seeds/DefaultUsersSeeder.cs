using CourtApp.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Identity.Seeds
{
    public static class DefaultUsers
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            await CreateUser(userManager, "superadmin@gmail.com", "Super", "Admin");
            await CreateUser(userManager, "lawyer@gmail.com", "Default", "Lawyer");
            await CreateUser(userManager, "corporate@gmail.com", "Default", "Corporate");
            await CreateUser(userManager, "associate@gmail.com", "Default", "Associate");
            await CreateUser(userManager, "clerk@gmail.com", "Default", "Clerk");
        }

        private static async Task CreateUser(
            UserManager<ApplicationUser> userManager,
            string email,
            string firstName,
            string lastName)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    IsActive = true
                };

                await userManager.CreateAsync(user, "123Pa$$word!");
            }
        }
    }

}
