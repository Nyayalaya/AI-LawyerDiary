using CourtApp.Domain.Enums;
using System;

namespace CourtApp.Application.Features.SystemUsers.DTOs
{
    /// <summary>
    /// DTO for system user response in list/detail views
    /// Maps directly from ApplicationUser
    /// </summary>
    public class SystemUserResponse
    {
        public string Id { get; set; }
        public string UserType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}".Trim();
        public string Email { get; set; }
        public DateTime RegisteredDate { get; set; }
        public string Subscription { get; set; }
        public DateTime? SubscriptionExpiryDate { get; set; }
        public string Status { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string StatusReason { get; set; }
    }
}
