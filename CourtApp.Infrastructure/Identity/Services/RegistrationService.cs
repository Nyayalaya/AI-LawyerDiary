using CourtApp.Application.DTOs.Mail;
using CourtApp.Application.Features.Auth.Dto;
using CourtApp.Application.Features.Auth.Services;
using CourtApp.Application.Interfaces.Shared;
using CourtApp.Domain.Enums;
using CourtApp.Infrastructure.Email.Templates;
using CourtApp.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
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

        public RegistrationService(
            UserManager<ApplicationUser> userManager,
            IMailService mailService,
            ILogger<RegistrationService> logger)
        {
            _userManager = userManager;
            _mailService = mailService;
            _logger = logger;
        }

        public async Task<string> RegisterAsync(RegisterRequest request)
        {
            // 🔹 Normalize
            var email = request.Email.Trim().ToLower();
            var phone = request.PhoneNumber.Trim();

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

            // 🔹 Send registration approval pending email (fire-and-forget)
            _ = SendApprovalPendingEmailAsync(user);

            // 🔹 Return user Id
            return user.Id;
        }

        private async Task SendApprovalPendingEmailAsync(ApplicationUser user)
        {
            try
            {
                var fullName = user.UserType == RegisterType.Lawyer
                    ? $"{user.FirstName} {user.LastName}".Trim()
                    : user.CompanyName;

                var emailBody = RegistrationEmailTemplate.GetApprovalPendingTemplate(fullName);

                var mailRequest = new MailRequest
                {
                    To = user.Email,
                    Subject = "Registration Pending Approval - Court App",
                    Body = emailBody,
                    IsHtml = true
                };

                await _mailService.SendAsync(mailRequest);
                _logger.LogInformation("Approval pending email sent to {Email}", user.Email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send approval pending email to {Email}", user.Email);
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
                DateOfBirth = null,
                Gender = default,
                ProfileImageUrl = null,
                IsActive = false // Pending admin approval
            };

            if (request.UserType == RegisterType.Lawyer)
            {
                user.FirstName = request.FirstName?.Trim();
                user.LastName = request.LastName?.Trim();
                user.EnrollmentNumber = request.EnrollmentNumber?.Trim();
            }
            else if (request.UserType == RegisterType.Corporate)
            {
                user.CompanyName = request.CompanyName?.Trim();
                user.RegistrationNumber = request.RegistrationNumber?.Trim();
            }

            return user;
        }
    }
}
