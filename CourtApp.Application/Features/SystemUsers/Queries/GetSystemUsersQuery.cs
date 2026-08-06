using CourtApp.Application.Common;
using CourtApp.Application.Features.SystemUsers.DTOs;
using CourtApp.Domain.Enums;
using MediatR;

namespace CourtApp.Application.Features.SystemUsers.Queries
{
    /// <summary>
    /// Query to get all registered system users with pagination and filtering
    /// </summary>
    public class GetSystemUsersQuery : IRequest<PaginatedResult<SystemUserResponse>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public RegisterType? UserType { get; set; }
        public UserAccountStatus? Status { get; set; }
        public string SearchTerm { get; set; }
    }
}
