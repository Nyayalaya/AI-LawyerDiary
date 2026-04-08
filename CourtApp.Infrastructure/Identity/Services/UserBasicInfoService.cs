using CourtApp.Application.Features.Auth.Services;
using CourtApp.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Identity.Services
{
    public class UserBasicInfoService : IUserBasicInfoService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<UserBasicInfoService> _logger;

        public UserBasicInfoService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        
        public async Task<bool> IsContactExistAsync(string contact)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(contact))
                    return false;

                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == contact);
                return user != null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in IsContactExistAsync: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> IsEmailExistAsync(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                    return false;

                var user = await _userManager.FindByEmailAsync(email);
                return user != null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in IsEmailExistAsync: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> IsEnrollmentExistAsync(string enrollement)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(enrollement))
                    return false;

                var user = await _userManager.Users
                    .FirstOrDefaultAsync(u => u.ProfessionalInfo != null && u.ProfessionalInfo.EnrollmentNo == enrollement);
                return user != null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in IsEnrollmentExistAsync: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> IsRegistrationNumberExistAsync(string registrationNumber)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(registrationNumber))
                    return false;

                var user = await _userManager.Users
                    .FirstOrDefaultAsync(u => u.RegistrationNumber != null && u.RegistrationNumber == registrationNumber);
                return user != null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in IsEnrollmentExistAsync: {ex.Message}");
                return false;
            }
        }
    }
}
