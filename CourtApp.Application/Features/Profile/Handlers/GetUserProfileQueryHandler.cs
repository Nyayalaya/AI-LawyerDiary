using CourtApp.Application.Common;
using CourtApp.Application.Features.Profile.Commands;
using CourtApp.Application.Features.Profile.DTOs;
using CourtApp.Application.Features.Profile.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Profile.Handlers
{
    public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, Result<UserProfileDto>>
    {
        private readonly IUserProfileService _userProfileService;

        public GetUserProfileQueryHandler(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        public async Task<Result<UserProfileDto>> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.UserId))
                return Result<UserProfileDto>.Fail("User ID is required");

            var profile = await _userProfileService.GetProfileAsync(request.UserId);

            if (profile == null)
                return Result<UserProfileDto>.Fail("User profile not found");

            return Result<UserProfileDto>.Success(profile);
        }
    }
}
