using CourtApp.Application.Common;
using CourtApp.Application.Features.SystemUsers.DTOs;
using CourtApp.Application.Features.SystemUsers.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.SystemUsers.Queries.Handlers
{
    /// <summary>
    /// Handler for GetSystemUserByIdQuery
    /// </summary>
    public class GetSystemUserByIdQueryHandler : IRequestHandler<GetSystemUserByIdQuery, Result<SystemUserResponse>>
    {
        private readonly ISystemUserService _systemUserService;

        public GetSystemUserByIdQueryHandler(ISystemUserService systemUserService)
        {
            _systemUserService = systemUserService;
        }

        public async Task<Result<SystemUserResponse>> Handle(GetSystemUserByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _systemUserService.GetSystemUserByIdAsync(request.UserId);

                if (user == null)
                {
                    return Result<SystemUserResponse>.Fail("System user not found.");
                }

                return Result<SystemUserResponse>.Success(user);
            }
            catch (Exception ex)
            {
                return Result<SystemUserResponse>.Fail($"Failed to retrieve system user: {ex.Message}");
            }
        }
    }
}
