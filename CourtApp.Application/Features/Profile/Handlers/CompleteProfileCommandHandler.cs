using CourtApp.Application.Common;
using CourtApp.Application.Features.Profile.Commands;
using CourtApp.Application.Features.Profile.DTOs;
using CourtApp.Application.Features.Profile.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Profile.Handlers
{
    public class CompleteProfileCommandHandler : IRequestHandler<CompleteProfileCommand, Result<UserProfileDto>>
    {
        private readonly IUserProfileService _userProfileService;

        public CompleteProfileCommandHandler(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        public async Task<Result<UserProfileDto>> Handle(CompleteProfileCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.UserId))
                return Result<UserProfileDto>.Fail("User ID is required");

            if (request.Profile == null)
                return Result<UserProfileDto>.Fail("Profile data is required");

            var result = await _userProfileService.CompleteProfileAsync(request.UserId, request.Profile);

            if (result == null)
                return Result<UserProfileDto>.Fail("Failed to complete user profile");

            var profile = await _userProfileService.GetProfileAsync(request.UserId);
            if (profile == null)
                return Result<UserProfileDto>.Fail("Failed to retrieve completed profile");

            return Result<UserProfileDto>.Success(profile);
        }
    }
}
