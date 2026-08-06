using CourtApp.Application.Common;
using CourtApp.Application.Features.SystemUsers.DTOs;
using MediatR;

namespace CourtApp.Application.Features.SystemUsers.Queries
{
    /// <summary>
    /// Query to get a specific system user by ID
    /// </summary>
    public class GetSystemUserByIdQuery : IRequest<Result<SystemUserResponse>>
    {
        public string UserId { get; set; }

        public GetSystemUserByIdQuery(string userId)
        {
            UserId = userId;
        }
    }
}
