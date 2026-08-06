using CourtApp.Application.Common;
using CourtApp.Application.Features.Profile.Commands;
using CourtApp.Application.Features.Profile.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Profile.Handlers
{
    public class UpdateUserOrganizationCommandHandler : IRequestHandler<UpdateUserOrganizationCommand, Result<bool>>
    {
        private readonly IUserOrganizationService _userOrganizationService;

        public UpdateUserOrganizationCommandHandler(IUserOrganizationService userOrganizationService)
        {
            _userOrganizationService = userOrganizationService;
        }

        public async Task<Result<bool>> Handle(UpdateUserOrganizationCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.UserId))
                return Result<bool>.Fail("User ID is required");

            if (request.Organization == null)
                return Result<bool>.Fail("Organization data is required");

            var result = await _userOrganizationService.MapUserAsync(request.Organization);

            if (!result.Succeeded)
                return Result<bool>.Fail(result.Message);

            return Result<bool>.Success(true);
        }
    }
}
