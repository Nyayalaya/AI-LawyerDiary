using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Common
{
    public sealed class UserContextInfo
    {
        public string? UserId { get; init; }
        public string? UserName { get; init; }
        public string? Email { get; init; }
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public string? FullName { get; init; }
        public string? Mobile { get; init; }
        public string IpAddress { get; init; } = "Unknown";
        public string CorrelationId { get; init; } = string.Empty;
        public bool IsAuthenticated { get; init; }
        public List<string> Roles { get; init; } = new();
        public IEnumerable<Claim> Claims { get; init; } = new List<Claim>();
    }
}
