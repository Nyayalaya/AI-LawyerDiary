using CourtApp.Application.Common;
using CourtApp.Application.Features.Profile.Commands;
using CourtApp.Application.Features.Profile.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Profile.Handlers
{
    public class AddUserBillingInfoCommandHandler : IRequestHandler<AddUserBillingInfoCommand, Result<bool>>
    {
        private readonly IUserBillingInfoService _userBillingInfoService;

        public AddUserBillingInfoCommandHandler(IUserBillingInfoService userBillingInfoService)
        {
            _userBillingInfoService = userBillingInfoService;
        }

        public async Task<Result<bool>> Handle(AddUserBillingInfoCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.UserId))
                return Result<bool>.Fail("User ID is required");

            if (request.BillingInfo == null)
                return Result<bool>.Fail("Billing information is required");

            var result = await _userBillingInfoService.SaveBillingInfo(request.UserId, request.BillingInfo);

            if (result == null)
                return Result<bool>.Fail("Failed to add user billing information");

            return Result<bool>.Success(true);
        }
    }
}
