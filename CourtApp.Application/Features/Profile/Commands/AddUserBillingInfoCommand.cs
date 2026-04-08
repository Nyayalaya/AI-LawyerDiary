using CourtApp.Application.Common;
using CourtApp.Application.Features.Profile.DTOs;
using MediatR;

namespace CourtApp.Application.Features.Profile.Commands
{
    public class AddUserBillingInfoCommand : IRequest<Result<bool>>
    {
        public string UserId { get; set; }
        public UserBillingInfoDto BillingInfo { get; set; }

        public AddUserBillingInfoCommand(string userId, UserBillingInfoDto billingInfo)
        {
            UserId = userId;
            BillingInfo = billingInfo;
        }
    }
}
