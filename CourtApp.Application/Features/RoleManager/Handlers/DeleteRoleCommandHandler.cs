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
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, Result<bool>>
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public DeleteRoleCommandHandler(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<Result<bool>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(request.Id);
                if (role == null)
                {
                    return Result<bool>.Fail("Role not found");
                }

                var result = await _roleManager.DeleteAsync(role);

                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    return Result<bool>.Fail($"Failed to delete role: {errors}");
                }

                return Result<bool>.Success(true, $"Role '{role.Name}' deleted successfully");
            }
            catch (Exception ex)
            {
                return Result<bool>.Fail($"An error occurred while deleting the role: {ex.Message}");
            }
        }
    }
}
