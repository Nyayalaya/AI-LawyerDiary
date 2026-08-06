using AuditTrail.Abstrations;
using CourtApp.Domain.Enums;
using System;

namespace CourtApp.Infrastructure.Identity.Models
{
    /// <summary>
    /// SystemUser entity for tracking subscription and registration details
    /// Links to ApplicationUser via UserId for storing subscription-related information
    /// </summary>
    public class SystemUser:AuditableEntity
    {
       
        /// <summary>
        /// Foreign key to ApplicationUser.Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Subscription details
        /// </summary>
        public SubscriptionType Subscription { get; set; } = SubscriptionType.Trial;
        public DateTime? SubscriptionExpiryDate { get; set; }
        public DateTime? SubscriptionStartDate { get; set; }

        /// <summary>
        /// Account status and tracking
        /// </summary>
        public UserAccountStatus Status { get; set; } = UserAccountStatus.Pending;
        public string? StatusReason { get; set; }
        public DateTime RegisteredDate { get; set; } = DateTime.UtcNow;
        public DateTime? LastLoginDate { get; set; }
        public bool IsEmailVerified { get; set; } = false;

        public ApplicationUser User { get; set; }
    }
}
