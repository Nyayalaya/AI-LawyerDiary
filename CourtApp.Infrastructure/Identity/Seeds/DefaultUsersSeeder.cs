using CourtApp.Domain.Enums;
using CourtApp.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

public static class DefaultUserSeeder
{
    public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
    {
        await CreateUser(userManager, RegisterType.SuperAdmin, "superadmin@gmail.com");
        await CreateUser(userManager, RegisterType.Lawyer, "lawyer@gmail.com");
        await CreateUser(userManager, RegisterType.Corporate, "corporate@gmail.com");
        await CreateUser(userManager, RegisterType.Associate, "associate@gmail.com");
        await CreateUser(userManager, RegisterType.Clerk, "clerk@gmail.com");
    }

    private static async Task CreateUser(
        UserManager<ApplicationUser> userManager,
        RegisterType registerType,
        string email)
    {
        email = email.Trim().ToLower();

        var existingUser = await userManager.FindByEmailAsync(email);
        if (existingUser != null)
            return;

       
        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            PhoneNumber = "9999999999",

            UserType = registerType,

            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            IsActive = true,

            ProfileImageUrl = null,
            DateOfBirth = null,
            Gender = default
        };

       
        if (registerType != RegisterType.Corporate)
        {
            user.FirstName = registerType.ToString();
            user.LastName = "User";

            // Optional: Only for Lawyer
            if (registerType == RegisterType.Lawyer)
            {
                user.EnrollmentNumber = $"ENR-{Guid.NewGuid().ToString().Substring(0, 8)}";
            }
        }

       
        if (registerType == RegisterType.Corporate)
        {
            user.CompanyName = "Default Company Pvt Ltd";
            user.RegistrationNumber = $"REG-{Guid.NewGuid().ToString().Substring(0, 8)}";
            user.FirstName = null;
            user.LastName = null;
        }

       
        var result = await userManager.CreateAsync(user, "123Pa$$word!");

        if (!result.Succeeded)
        {
            throw new Exception($"User creation failed: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }

        
        var role = registerType.ToString(); 

        var roleResult = await userManager.AddToRoleAsync(user, role);

        if (!roleResult.Succeeded)
        {
            throw new Exception($"Role assignment failed: {string.Join(", ", roleResult.Errors.Select(e => e.Description))}");
        }
    }
}
