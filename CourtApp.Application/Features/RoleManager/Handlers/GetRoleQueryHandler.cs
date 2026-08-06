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
    public class GetRoleQueryHandler : IRequestHandler<GetRoleQuery, PaginatedResult<RoleResponse>>
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public GetRoleQueryHandler(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<PaginatedResult<RoleResponse>> Handle(GetRoleQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var query = _roleManager.Roles.AsQueryable();

                // Apply search filter
                if (!string.IsNullOrWhiteSpace(request.SearchTerm))
                {
                    query = query.Where(r => r.Name.Contains(request.SearchTerm));
                }

                var totalCount = query.Count();

                // Apply pagination
                var roles = query
                    .OrderBy(r => r.Name)
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToList();

                var roleResponses = new List<RoleResponse>();

                foreach (var role in roles)
                {
                    var permissions = await _roleManager.GetClaimsAsync(role);

                    roleResponses.Add(new RoleResponse
                    {
                        Id = role.Id,
                        Name = role.Name,
                        Description = role.ConcurrencyStamp ?? "",
                        PermissionCount = permissions.Count,
                        UserCount = 0  // User count will be set from API layer if needed
                    });
                }

                return PaginatedResult<RoleResponse>.Success(
                    roleResponses,
                    totalCount,
                    request.PageNumber,
                    request.PageSize,
                    "Roles retrieved successfully");
            }
            catch (Exception ex)
            {
                return PaginatedResult<RoleResponse>.Failure($"An error occurred while retrieving roles: {ex.Message}");
            }
        }
    }
}
