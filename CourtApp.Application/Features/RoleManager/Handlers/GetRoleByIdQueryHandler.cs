using CourtApp.Application.Common;
using CourtApp.Application.Features.RoleManager.Queries;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.RoleManager.Handlers
{
    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, Result<RoleDetailResponse>>
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public GetRoleByIdQueryHandler(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<Result<RoleDetailResponse>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(request.Id);

                if (role == null)
                {
                    return Result<RoleDetailResponse>.Fail("Role not found");
                }

                var response = new RoleDetailResponse
                {
                    Id = role.Id,
                    Name = role.Name,
                    Description = role.ConcurrencyStamp ?? ""
                };

                return Result<RoleDetailResponse>.Success(response, "Role retrieved successfully");
            }
            catch (Exception ex)
            {
                return Result<RoleDetailResponse>.Fail($"An error occurred while retrieving the role: {ex.Message}");
            }
        }
    }
}
