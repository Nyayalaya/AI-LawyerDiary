using CourtApp.Application.Features.Auth.Services;
using CourtApp.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Identity.Services
{
    public class ChangePasswordService : IChangePasswordService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ChangePasswordService> _logger;
        public ChangePasswordService(UserManager<ApplicationUser> userManager,
            ILogger<ChangePasswordService> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }
        public async Task<string> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new Exception("User not found");

            var result = await _userManager.ChangePasswordAsync(
                        user,
                        currentPassword,
                        newPassword);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception(errors);
            }

            // 🔥 Invalidate old tokens
            await _userManager.UpdateSecurityStampAsync(user);

            return "Password changed successfully";
        }
    }
}
