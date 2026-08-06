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
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Result<Guid>>
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public CreateRoleCommandHandler(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<Result<Guid>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var newRole = new IdentityRole
                {
                    Name = request.Name,
                    NormalizedName = request.Name.ToUpper()
                };

                var result = await _roleManager.CreateAsync(newRole);

                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    return Result<Guid>.Fail($"Failed to create role: {errors}");
                }

                return Result<Guid>.Success(Guid.Parse(newRole.Id), $"Role '{request.Name}' created successfully");
            }
            catch (Exception ex)
            {
                return Result<Guid>.Fail($"An error occurred while creating the role: {ex.Message}");
            }
        }
    }
}
