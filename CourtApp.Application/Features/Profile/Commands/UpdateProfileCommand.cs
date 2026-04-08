using CourtApp.Application.Common;
using CourtApp.Application.Features.Profile.DTOs;
using MediatR;

namespace CourtApp.Application.Features.Profile.Commands
{
    public class UpdateProfileCommand : IRequest<Result<UserProfileDto>>
    {
        public UpdateProfileRequest Profile { get; set; }

        public UpdateProfileCommand(UpdateProfileRequest profile)
        {
            Profile = profile;
        }
    }
}
