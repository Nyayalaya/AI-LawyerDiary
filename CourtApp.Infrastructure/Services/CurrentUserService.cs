using CourtApp.Application.Common;
using CourtApp.Application.Constants;
using CourtApp.Application.Interfaces.Shared;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Services
{
    public sealed class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
            => _httpContextAccessor = httpContextAccessor;

        public UserContextInfo GetCurrentUser()
        {
            var context = _httpContextAccessor.HttpContext;
            if (context is null) return new UserContextInfo();

            var user = context.User;
            var isAuthenticated = user?.Identity?.IsAuthenticated ?? false;

            if (!isAuthenticated)
                return new UserContextInfo
                {
                    IpAddress = GetClientIp(context),
                    CorrelationId = context.TraceIdentifier
                };

            var firstName = user!.FindFirst(AppClaimTypes.FirstName)?.Value
                         ?? user.FindFirst(ClaimTypes.GivenName)?.Value;
            var lastName = user.FindFirst(AppClaimTypes.LastName)?.Value
                         ?? user.FindFirst(ClaimTypes.Surname)?.Value;

            return new UserContextInfo
            {
                IsAuthenticated = true,
                IpAddress = GetClientIp(context),
                CorrelationId = context.TraceIdentifier,
                UserId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
                               ?? user.FindFirst(AppClaimTypes.UserId)?.Value,
                UserName = user.FindFirst(ClaimTypes.Name)?.Value
                               ?? user.FindFirst(AppClaimTypes.UserName)?.Value,
                Email = user.FindFirst(ClaimTypes.Email)?.Value
                               ?? user.FindFirst(AppClaimTypes.Email)?.Value,
                FirstName = firstName,
                LastName = lastName,
                FullName = user.FindFirst(AppClaimTypes.FullName)?.Value
                               ?? BuildFullName(firstName, lastName),
                Mobile = user.FindFirst(AppClaimTypes.Mobile)?.Value
                               ?? user.FindFirst(ClaimTypes.MobilePhone)?.Value,
                Roles = user.FindAll(AppClaimTypes.Roles)
                                      .Select(c => c.Value).ToList(),
                Claims = user.Claims
            };
        }

        public bool HasRole(string role) => GetCurrentUser().Roles.Contains(role);
        public bool HasAnyRole(params string[] roles) => GetCurrentUser().Roles.Intersect(roles).Any();

        private static string GetClientIp(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue("X-Forwarded-For", out var fwd))
            {
                var ip = fwd.FirstOrDefault()?.Split(',').FirstOrDefault()?.Trim();
                if (!string.IsNullOrEmpty(ip)) return ip;
            }
            if (context.Request.Headers.TryGetValue("X-Real-IP", out var real))
            {
                var ip = real.FirstOrDefault()?.Trim();
                if (!string.IsNullOrEmpty(ip)) return ip;
            }
            return context.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
        }

        private static string? BuildFullName(string? first, string? last)
        {
            var full = $"{first} {last}".Trim();
            return string.IsNullOrWhiteSpace(full) ? null : full;
        }
    }
}
