using CourtApp.Domain.Enums;

namespace CourtApp.Application.Features.SystemUsers.DTOs
{
    /// <summary>
    /// DTO for updating system user subscription and status
    /// </summary>
    public class UpdateSystemUserRequest
    {
        public SubscriptionType? Subscription { get; set; }
        public UserAccountStatus? Status { get; set; }
        public string StatusReason { get; set; }
    }
}
