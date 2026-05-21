using CourtApp.Application.Common;
using CourtApp.Application.Features.SystemUsers.DTOs;
using CourtApp.Application.Features.SystemUsers.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.SystemUsers.Queries.Handlers
{
    /// <summary>
    /// Handler for GetSystemUsersQuery
    /// </summary>
    public class GetSystemUsersQueryHandler : IRequestHandler<GetSystemUsersQuery, PaginatedResult<SystemUserResponse>>
    {
        private readonly ISystemUserService _systemUserService;

        public GetSystemUsersQueryHandler(ISystemUserService systemUserService)
        {
            _systemUserService = systemUserService;
        }

        public async Task<PaginatedResult<SystemUserResponse>> Handle(GetSystemUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var users = await _systemUserService.GetAllRegisteredUsersAsync(
                    userType: request.UserType,
                    status: request.Status,
                    pageNumber: request.PageNumber,
                    pageSize: request.PageSize,
                    searchTerm: request.SearchTerm
                );

                var totalCount = users.Any() ? users.Count : 0;

                var paginatedResult = PaginatedResult<SystemUserResponse>.Success(
                    data: users,
                    totalCount: totalCount,
                    pageNumber: request.PageNumber,
                    pageSize: request.PageSize
                );

                return paginatedResult;
            }
            catch (Exception ex)
            {
                return PaginatedResult<SystemUserResponse>.Failure(
                    $"Failed to retrieve system users: {ex.Message}"
                );
            }
        }
    }
}
