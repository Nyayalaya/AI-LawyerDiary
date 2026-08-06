using CourtApp.Application.Common;
using CourtApp.Application.Features.Profile.Commands;
using CourtApp.Application.Features.Profile.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Profile.Handlers
{
    public class CreateOrganizationCommandHandler : IRequestHandler<CreateOrganizationCommand, Result<System.Guid>>
    {
        private readonly IUserOrganizationService _userOrganizationService;

        public CreateOrganizationCommandHandler(IUserOrganizationService userOrganizationService)
        {
            _userOrganizationService = userOrganizationService;
        }

        public async Task<Result<System.Guid>> Handle(CreateOrganizationCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.UserId))
                return Result<System.Guid>.Fail("User ID is required");

            if (request.Organization == null)
                return Result<System.Guid>.Fail("Organization data is required");

            request.Organization.OwnerUserId = request.UserId;
            var result = await _userOrganizationService.CreateOrganizationAsync(request.Organization);

            if (!result.Succeeded)
                return Result<System.Guid>.Fail(result.Message);

            if (!System.Guid.TryParse(result.Data, out var organizationId))
                return Result<System.Guid>.Fail("Failed to parse organization ID");

            return Result<System.Guid>.Success(organizationId);
        }
    }
}
