using CourtApp.Application.Features.RoleManager.Commands;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.RoleManager.Validators
{
    public class CreateRoleValidator : AbstractValidator<CreateRoleCommand>
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public CreateRoleValidator(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Role name is required")
                .MinimumLength(3).WithMessage("Role name must be at least 3 characters")
                .MaximumLength(100).WithMessage("Role name must not exceed 100 characters")
                .MustAsync(BeUniqueName).WithMessage("A role with this name already exists");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters");
        }

        private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
        {
            var roleExists = await _roleManager.FindByNameAsync(name);
            return roleExists == null;
        }
    }
}
