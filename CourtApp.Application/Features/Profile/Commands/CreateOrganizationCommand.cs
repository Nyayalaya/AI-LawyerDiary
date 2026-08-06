using CourtApp.Application.Common;
using CourtApp.Application.Features.Profile.DTOs;
using MediatR;
using System;

namespace CourtApp.Application.Features.Profile.Commands
{
    public class CreateOrganizationCommand : IRequest<Result<Guid>>
    {
        public string UserId { get; set; }
        public CreateOrganizationRequest Organization { get; set; }

        public CreateOrganizationCommand(string userId, CreateOrganizationRequest organization)
        {
            UserId = userId;
            Organization = organization;
        }
    }
}
