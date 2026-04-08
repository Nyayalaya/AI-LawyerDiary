using CourtApp.Application.Common;
using CourtApp.Application.Features.Profile.DTOs;
using MediatR;

namespace CourtApp.Application.Features.Profile.Commands
{
    public class UpdateUserOrganizationCommand : IRequest<Result<bool>>
    {
        public string UserId { get; set; }
        public UserOrganizationRequest Organization { get; set; }

        public UpdateUserOrganizationCommand(string userId, UserOrganizationRequest organization)
        {
            UserId = userId;
            Organization = organization;
        }
    }
}
