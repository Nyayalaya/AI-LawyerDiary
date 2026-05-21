using CourtApp.Application.Features.RoleManager.Commands;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.RoleManager.Validators
{
    public class UpdateRolePermissionsValidator : AbstractValidator<UpdateRolePermissionsCommand>
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public UpdateRolePermissionsValidator(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;

            RuleFor(x => x.RoleId)
                .NotEmpty().WithMessage("Role ID is required")
                .MustAsync(RoleExists).WithMessage("Role does not exist");

            RuleFor(x => x.Permissions)
                .NotEmpty().WithMessage("At least one permission must be provided");

            RuleForEach(x => x.Permissions).ChildRules(permission =>
            {
                permission.RuleFor(p => p.PermissionId)
                    .NotEmpty().WithMessage("Permission ID is required");
            });
        }

        private async Task<bool> RoleExists(string id, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByIdAsync(id);
            return role != null;
        }
    }
}
