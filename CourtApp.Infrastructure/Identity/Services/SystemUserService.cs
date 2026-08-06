using CourtApp.Application.Features.SystemUsers.DTOs;
using CourtApp.Application.Features.SystemUsers.Services;
using CourtApp.Domain.Enums;
using CourtApp.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Identity.Services
{
    public class SystemUserService : ISystemUserService
    {
        private readonly IdentityContext _identityContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public SystemUserService(
            IdentityContext identityContext,
            UserManager<ApplicationUser> userManager)
        {
            _identityContext = identityContext;
            _userManager = userManager;
        }

        public async Task<List<SystemUserResponse>> GetAllRegisteredUsersAsync(
            RegisterType? userType = null,
            UserAccountStatus? status = null,
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null)
        {
            var query = _identityContext.SystemUsers
                .AsNoTracking()
                .Include(x => x.User)
                .AsQueryable();

            if (userType.HasValue)
                query = query.Where(x => x.User.UserType == userType);

            if (status.HasValue)
                query = query.Where(x => x.Status == status);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.Trim().ToLower();

                query = query.Where(x =>
                    x.User.FirstName.ToLower().Contains(searchTerm) ||
                    x.User.LastName.ToLower().Contains(searchTerm) ||
                    x.User.Email.ToLower().Contains(searchTerm));
            }

            return await query
                .OrderByDescending(x => x.RegisteredDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new SystemUserResponse
                {
                    Id = x.UserId,
                    UserType = x.User.UserType.ToString(),
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName,
                    Email = x.User.Email,
                    RegisteredDate = x.RegisteredDate,
                    Subscription = x.Subscription.ToString(),
                    SubscriptionExpiryDate = x.SubscriptionExpiryDate,
                    Status = x.Status.ToString(),
                    LastLoginDate = x.LastLoginDate,
                    StatusReason = x.StatusReason
                })
                .ToListAsync();
        }

        public async Task<SystemUserResponse?> GetSystemUserByIdAsync(string userId)
        {
            return await _identityContext.SystemUsers
                .AsNoTracking()
                .Include(x => x.User)
                .Where(x => x.UserId == userId)
                .Select(x => new SystemUserResponse
                {
                    Id = x.UserId,
                    UserType = x.User.UserType.ToString(),
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName,
                    Email = x.User.Email,
                    RegisteredDate = x.RegisteredDate,
                    Subscription = x.Subscription.ToString(),
                    SubscriptionExpiryDate = x.SubscriptionExpiryDate,
                    Status = x.Status.ToString(),
                    LastLoginDate = x.LastLoginDate,
                    StatusReason = x.StatusReason
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateSystemUserAsync(
            string userId,
            UpdateSystemUserRequest request)
        {
            var systemUser = await _identityContext.SystemUsers
                .FirstOrDefaultAsync(x => x.UserId == userId);

            if (systemUser == null)
                return false;

            if (request.Subscription.HasValue)
                systemUser.Subscription = request.Subscription.Value;

            if (request.Status.HasValue)
                systemUser.Status = request.Status.Value;

            if (!string.IsNullOrWhiteSpace(request.StatusReason))
                systemUser.StatusReason = request.StatusReason;
            await _identityContext.SaveChangesAsync();

            return true;
        }

        public async Task<Dictionary<RegisterType, int>> GetUserCountByTypeAsync()
        {
            return await _identityContext.SystemUsers
                .AsNoTracking()
                .Include(x => x.User)
                .GroupBy(x => x.User.UserType)
                .ToDictionaryAsync(
                    x => x.Key,
                    x => x.Count());
        }

        public async Task<int> GetActiveUsersCountAsync()
        {
            return await _identityContext.SystemUsers
                .AsNoTracking()
                .CountAsync(x => x.Status == UserAccountStatus.Active);
        }
    }
}