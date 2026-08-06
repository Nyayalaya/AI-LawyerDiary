using CourtApp.Application.Features.RoleManager.Commands;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.RoleManager.Validators
{
    public class UpdateRoleValidator : AbstractValidator<UpdateRoleCommand>
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public UpdateRoleValidator(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Role ID is required")
                .MustAsync(RoleExists).WithMessage("Role does not exist");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Role name is required")
                .MinimumLength(3).WithMessage("Role name must be at least 3 characters")
                .MaximumLength(100).WithMessage("Role name must not exceed 100 characters")
                .MustAsync(BeUniqueName).WithMessage("A role with this name already exists");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters");
        }

        private async Task<bool> RoleExists(string id, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByIdAsync(id);
            return role != null;
        }

        private async Task<bool> BeUniqueName(UpdateRoleCommand command, string name, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByNameAsync(name);
            return role == null || role.Id == command.Id;
        }
    }
}
