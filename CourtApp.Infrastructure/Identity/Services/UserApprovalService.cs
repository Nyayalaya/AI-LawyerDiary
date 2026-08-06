using CourtApp.Application.DTOs.Mail;
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
    public class UserApprovalService : IUserApprovalService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMailService _mailService;
        private readonly ILogger<UserApprovalService> _logger;

        public UserApprovalService(
            UserManager<ApplicationUser> userManager,
            IMailService mailService,
            ILogger<UserApprovalService> logger)
        {
            _userManager = userManager;
            _mailService = mailService;
            _logger = logger;
        }

        public async Task<string> ApproveUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new Exception("User not found.");

            if (user.IsActive)
                throw new Exception("User already approved.");

            // 🔹 Activate user
            user.IsActive = true;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception(errors);
            }

            // 🔹 Fire-and-forget email
            _ = SendApprovalEmailAsync(user);

            return "User approved successfully.";
        }

        public async Task<string> RejectUserAsync(string userId, string reason)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new Exception("User not found.");

            // 🔹 Keep inactive or mark rejected (based on your design)
            user.IsActive = false;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception(errors);
            }

            // 🔹 Fire-and-forget rejection email
            _ = SendRejectionEmailAsync(user, reason);

            return "User rejected successfully.";
        }

        // ================= EMAIL METHODS =================

        private async Task SendApprovalEmailAsync(ApplicationUser user)
        {
            try
            {
                var name = user.UserType == RegisterType.Lawyer
                    ? $"{user.FirstName} {user.LastName}".Trim()
                    : user.CompanyName;

                var body = RegistrationEmailTemplate.GetApprovalConfirmedTemplate(name);

                var mailRequest = new MailRequest
                {
                    To = user.Email,
                    Subject = "Your Account Has Been Approved - Court App",
                    Body = body,
                    IsHtml = true
                };

                await _mailService.SendAsync(mailRequest);

                _logger.LogInformation("Approval email sent to {Email}", user.Email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending approval email to {Email}", user.Email);
            }
        }

        private async Task SendRejectionEmailAsync(ApplicationUser user, string reason)
        {
            try
            {
                var name = user.UserType == RegisterType.Lawyer
                    ? $"{user.FirstName} {user.LastName}".Trim()
                    : user.CompanyName;

                var body = RegistrationEmailTemplate.GetRejectionTemplate(name, reason);

                var mailRequest = new MailRequest
                {
                    To = user.Email,
                    Subject = "Your Registration Was Rejected - Court App",
                    Body = body,
                    IsHtml = true
                };

                await _mailService.SendAsync(mailRequest);

                _logger.LogInformation("Rejection email sent to {Email}", user.Email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending rejection email to {Email}", user.Email);
            }
        }
    }
}
