using CourtApp.Application.Common;
using CourtApp.Application.Features.SystemUsers.Commands;
using CourtApp.Application.Features.SystemUsers.Services;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.SystemUsers.Commands.Handlers
{
    /// <summary>
    /// Handler for UpdateSystemUserCommand
    /// </summary>
    public class UpdateSystemUserCommandHandler : IRequestHandler<UpdateSystemUserCommand, Result<bool>>
    {
        private readonly ISystemUserService _systemUserService;

        public UpdateSystemUserCommandHandler(ISystemUserService systemUserService)
        {
            _systemUserService = systemUserService;
        }

        public async Task<Result<bool>> Handle(UpdateSystemUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _systemUserService.UpdateSystemUserAsync(request.UserId, request.Request);

                if (!result)
                {
                    return Result<bool>.Fail("Failed to update system user.");
                }

                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Fail($"Failed to update system user: {ex.Message}");
            }
        }
    }
}
