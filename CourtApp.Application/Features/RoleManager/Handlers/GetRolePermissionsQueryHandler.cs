using CourtApp.Application.Common;
using CourtApp.Application.Features.RoleManager.Queries;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.RoleManager.Handlers
{
    public class GetRolePermissionsQueryHandler : IRequestHandler<GetRolePermissionsQuery, Result<List<RolePermissionResponse>>>
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public GetRolePermissionsQueryHandler(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<Result<List<RolePermissionResponse>>> Handle(GetRolePermissionsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(request.RoleId);

                if (role == null)
                {
                    return Result<List<RolePermissionResponse>>.Fail("Role not found");
                }

                // Get all claims for the role
                var claims = await _roleManager.GetClaimsAsync(role);

                // Filter permission claims
                var permissionClaims = claims
                    .Where(c => c.Type == "Permission")
                    .Select(c => new RolePermissionResponse
                    {
                        Id = c.Value,
                        Name = c.Value,
                        Description = "",
                        IsActive = true
                    })
                    .ToList();

                return Result<List<RolePermissionResponse>>.Success(
                    permissionClaims,
                    "Role permissions retrieved successfully");
            }
            catch (Exception ex)
            {
                return Result<List<RolePermissionResponse>>.Fail(
                    $"An error occurred while retrieving role permissions: {ex.Message}");
            }
        }
    }
}
