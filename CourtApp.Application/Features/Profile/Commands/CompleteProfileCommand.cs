using CourtApp.Application.Common;
using CourtApp.Application.Features.Profile.DTOs;
using MediatR;

namespace CourtApp.Application.Features.Profile.Commands
{
    public class CompleteProfileCommand : IRequest<Result<UserProfileDto>>
    {
        public string UserId { get; set; }
        public CompleteProfileRequest Profile { get; set; }

        public CompleteProfileCommand(string userId, CompleteProfileRequest profile)
        {
            UserId = userId;
            Profile = profile;
        }
    }
}
