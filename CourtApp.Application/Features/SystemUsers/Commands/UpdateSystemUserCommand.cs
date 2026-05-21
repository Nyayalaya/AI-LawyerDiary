using CourtApp.Application.Common;
using CourtApp.Application.Features.SystemUsers.DTOs;
using MediatR;

namespace CourtApp.Application.Features.SystemUsers.Commands
{
    /// <summary>
    /// Command to update system user subscription and status
    /// </summary>
    public class UpdateSystemUserCommand : IRequest<Result<bool>>
    {
        public string UserId { get; set; }
        public UpdateSystemUserRequest Request { get; set; }
    }
}
