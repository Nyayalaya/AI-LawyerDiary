using CourtApp.Application.Features.Profile.DTOs;
using CourtApp.Application.Features.Profile.Services;
using CourtApp.Infrastructure.Identity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Identity.Services
{
    public class UserBillingInfoService : IUserBillingInfoService
    {
        private readonly IdentityContext _db;
        private readonly ILogger<UserBillingInfoService> _logger;
        public UserBillingInfoService(IdentityContext db
            , ILogger<UserBillingInfoService> logger)
        {
            _db = db;
            _logger = logger;
        }
        public async Task<string> SaveBillingInfo(string userId, UserBillingInfoDto request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var billingInfo = new UserBillingModel
            {
                UserId = userId,
                AccountNumber = request.AccountNumber,
                IfscCode = request.IfscCode,
                Branch = request.Branch,
                Pan = request.Pan,
                GstNo = request.GstNo
            };

            var result=await _db.UserBillingInfos.AddAsync(billingInfo);
            await _db.SaveChangesAsync();
            return "Billing information saved successfully";

        }

        public async Task<string> UpdateBillingInfo(string userId, UserBillingInfoDto request)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("UserId is required");

            if (request == null)
                throw new ArgumentNullException(nameof(request));

            // 🔹 Check user exists
            var userExists = await _db.Users.AnyAsync(x => x.Id == userId);
            if (!userExists)
                throw new Exception("User not found");

            // 🔹 Fetch existing billing info
            var billingInfo = await _db.UserBillingInfos
                .FirstOrDefaultAsync(x => x.UserId == userId);

            if (billingInfo == null)
                throw new Exception("Billing information not found");

            // 🔹 Update fields (safe trim)
            billingInfo.AccountNumber = request.AccountNumber?.Trim();
            billingInfo.IfscCode = request.IfscCode?.Trim();
            billingInfo.Branch = request.Branch?.Trim();
            billingInfo.Pan = request.Pan?.Trim();
            billingInfo.GstNo = request.GstNo?.Trim();

            // 🔹 Save changes
            await _db.SaveChangesAsync();

            return "Billing information updated successfully";
        }
    }
}
