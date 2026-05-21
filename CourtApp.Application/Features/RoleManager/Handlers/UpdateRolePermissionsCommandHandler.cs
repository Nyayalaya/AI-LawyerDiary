using CourtApp.Application.Common;
using CourtApp.Application.Features.RoleManager.Commands;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.RoleManager.Handlers
{
    public class UpdateRolePermissionsCommandHandler : IRequestHandler<UpdateRolePermissionsCommand, Result<bool>>
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public UpdateRolePermissionsCommandHandler(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<Result<bool>> Handle(UpdateRolePermissionsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(request.RoleId);
                if (role == null)
                {
                    return Result<bool>.Fail("Role not found");
                }

                // Get all current claims for the role
                var existingClaims = await _roleManager.GetClaimsAsync(role);

                // Remove all existing permission claims
                foreach (var claim in existingClaims.Where(c => c.Type == "Permission"))
                {
                    await _roleManager.RemoveClaimAsync(role, claim);
                }

                // Add new permission claims
                foreach (var permission in request.Permissions)
                {
                    if (permission.IsActive)
                    {
                        var claimResult = await _roleManager.AddClaimAsync(role,
                            new System.Security.Claims.Claim("Permission", permission.PermissionId));

                        if (!claimResult.Succeeded)
                        {
                            return Result<bool>.Fail($"Failed to add permission '{permission.PermissionId}'");
                        }
                    }
                }

                return Result<bool>.Success(true, "Role permissions updated successfully");
            }
            catch (Exception ex)
            {
                return Result<bool>.Fail($"An error occurred while updating role permissions: {ex.Message}");
            }
        }
    }
}
