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
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, Result<bool>>
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public UpdateRoleCommandHandler(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<Result<bool>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(request.Id);
                if (role == null)
                {
                    return Result<bool>.Fail("Role not found");
                }

                role.Name = request.Name;
                role.NormalizedName = request.Name.ToUpper();

                var result = await _roleManager.UpdateAsync(role);

                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    return Result<bool>.Fail($"Failed to update role: {errors}");
                }

                return Result<bool>.Success(true, $"Role '{request.Name}' updated successfully");
            }
            catch (Exception ex)
            {
                return Result<bool>.Fail($"An error occurred while updating the role: {ex.Message}");
            }
        }
    }
}
