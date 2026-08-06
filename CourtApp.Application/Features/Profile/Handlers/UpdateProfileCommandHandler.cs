using CourtApp.Application.Common;
using CourtApp.Application.Features.Profile.Commands;
using CourtApp.Application.Features.Profile.DTOs;
using CourtApp.Application.Features.Profile.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Profile.Handlers
{
    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, Result<UserProfileDto>>
    {
        private readonly IUserProfileService _userProfileService;

        public UpdateProfileCommandHandler(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        public async Task<Result<UserProfileDto>> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            if (request.Profile == null)
                return Result<UserProfileDto>.Fail("Profile data is required");

            if (string.IsNullOrEmpty(request.Profile.UserId))
                return Result<UserProfileDto>.Fail("User ID is required");

            var result = await _userProfileService.UpdateProfileAsync(request.Profile.UserId, request.Profile);

            if (result == null)
                return Result<UserProfileDto>.Fail("Failed to update user profile");

            var profile = await _userProfileService.GetProfileAsync(request.Profile.UserId);
            if (profile == null)
                return Result<UserProfileDto>.Fail("Failed to retrieve updated profile");

            return Result<UserProfileDto>.Success(profile);
        }
    }
}
