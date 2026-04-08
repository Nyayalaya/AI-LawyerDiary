using CourtApp.Application.Common;
using CourtApp.Application.Features.Profile.DTOs;
using MediatR;

namespace CourtApp.Application.Features.Profile.Commands
{
    public class GetUserProfileQuery : IRequest<Result<UserProfileDto>>
    {
        public string UserId { get; set; }

        public GetUserProfileQuery(string userId)
        {
            UserId = userId;
        }
    }
}
