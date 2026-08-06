using CourtApp.Application.Common;
using CourtApp.Application.Features.Profile.DTOs;
using MediatR;
using System;

namespace CourtApp.Application.Features.Profile.Commands
{
    public class CreateSubUserCommand : IRequest<Result<string>>
    {
        public string ParentUserId { get; set; }
        public CreateSubUserRequest SubUser { get; set; }

        public CreateSubUserCommand(string parentUserId, CreateSubUserRequest subUser)
        {
            ParentUserId = parentUserId;
            SubUser = subUser;
        }
    }
}
