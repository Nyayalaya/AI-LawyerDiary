using CourtApp.Application.DTOs.Mail;
using CourtApp.Application.Features.Auth.Dto;
using CourtApp.Application.Features.Auth.Services;
using CourtApp.Application.Interfaces.Shared;
using CourtApp.Domain.Entities;
using CourtApp.Domain.Enums;
using CourtApp.Infrastructure.Email.Templates;
using CourtApp.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Identity.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMailService _mailService;
        private readonly ILogger<RegistrationService> _logger;
        private readonly IdentityContext _identityContext;

        public RegistrationService(
            UserManager<ApplicationUser> userManager,
            IMailService mailService,
            ILogger<RegistrationService> logger,
            IdentityContext identityContext)
        {
            _userManager = userManager;
            _mailService = mailService;
            _logger = logger;
            _identityContext = identityContext;
        }

        public async Task<string> RegisterAsync(RegisterRequest request)
        {
            // 🔹 Normalize
            var email = request.Email.Trim().ToLower();
            var phone = request.Contact.Trim();

            // 🔹 Set basic user info
            var user = SetUserRegistrationInfo(request, email, phone);

            // 🔹 Create user
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception(errors); // Let the handler return Result
            }

            // 🔹 Assign role
            var role = request.UserType == RegisterType.Lawyer ? "Lawyer" : "Corporate";
            var roleResult = await _userManager.AddToRoleAsync(user, role);

            if (!roleResult.Succeeded)
            {
                var errors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
                throw new Exception(errors);
            }

            // 🔹 Create SystemUser record with subscription details
            await CreateSystemUserRecordAsync(user, request);

            // 🔹 Send registration approval pending email with free plan info (fire-and-forget)
            _ = SendApprovalPendingEmailAsync(user);

            // 🔹 Return user Id
            return user.Id;
        }

        private async Task CreateSystemUserRecordAsync(ApplicationUser applicationUser, RegisterRequest request)
        {
            try
            {
                var freeplanExpiryDate = DateTime.UtcNow.AddMonths(1);

                var systemUser = new SystemUser
                {
                    UserId = applicationUser.Id,
                    Subscription = SubscriptionType.Free,
                    SubscriptionExpiryDate = freeplanExpiryDate,
                    SubscriptionStartDate = DateTime.UtcNow,
                    Status = UserAccountStatus.Pending, // Pending admin approval
                    StatusReason = "New registration - Free plan enabled for 1 month",
                    RegisteredDate = DateTime.UtcNow,
                    IsEmailVerified = false,
                    CreatedBy= applicationUser.Id,
                    CreatedOn=DateTime.Now
                };

                _identityContext.SystemUsers.Add(systemUser);
                await _identityContext.SaveChangesAsync();

                _logger.LogInformation("SystemUser record created for user {UserId} with free plan expiry on {ExpiryDate}", 
                    applicationUser.Id, freeplanExpiryDate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create SystemUser record for user {UserId}", applicationUser.Id);
                // Don't throw - we don't want registration to fail if SystemUser creation fails
            }
        }

        private async Task SendApprovalPendingEmailAsync(ApplicationUser user)
        {
            try
            {
                var fullName = user.UserType == RegisterType.Lawyer
                    ? $"{user.FirstName} {user.LastName}".Trim()
                    : user.CompanyName;

                // 🔹 Get free plan expiry date from SystemUser record
                var systemUser = await _identityContext.SystemUsers
                    .FirstOrDefaultAsync(x => x.UserId == user.Id);

                var expiryDate = systemUser?.SubscriptionExpiryDate ?? DateTime.UtcNow.AddMonths(1);

                // 🔹 Send registration approval + free plan info email
                var emailBody = RegistrationEmailTemplate.GetApprovalPendingWithFreePlanTemplate(fullName, expiryDate);

                var mailRequest = new MailRequest
                {
                    To = user.Email,
                    Subject = "Registration Successful - Free Plan Access for 1 Month - Court App",
                    Body = emailBody,
                    IsHtml = true
                };

                await _mailService.SendAsync(mailRequest);
                _logger.LogInformation("Registration email with free plan info sent to {Email}. Plan expires on {ExpiryDate}", user.Email, expiryDate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send registration email to {Email}", user.Email);
                // Do not throw, we don't want registration to fail due to email
            }
        }

        private ApplicationUser SetUserRegistrationInfo(RegisterRequest request, string email, string phone)
        {
            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                PhoneNumber = phone,
                UserType = request.UserType,
                DateOfBirth = request.IndividualInfoDto.DateOfBirth,
                Gender = request.IndividualInfoDto.Gender,
                ProfileImageUrl = null,
                IsActive = false // Pending admin approval
            };

            if (request.UserType == RegisterType.Lawyer)
            {
                user.FirstName = request.IndividualInfoDto.FirstName?.Trim();
                user.LastName = request.IndividualInfoDto.LastName?.Trim();
                user.EnrollmentNumber = request.IndividualInfoDto.EnrollmentNumber?.Trim();
            }
            else if (request.UserType == RegisterType.Corporate)
            {
                user.CompanyName = request.CompanyInfoDto.CompanyName?.Trim();
                user.RegistrationNumber = request.CompanyInfoDto.RegistrationNumber?.Trim();
            }

            return user;
        }
    }
}
