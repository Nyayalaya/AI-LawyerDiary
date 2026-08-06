using CourtApp.Application.Common;
using CourtApp.Application.Features.Auth.Commands;
using CourtApp.Application.Features.Auth.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Auth.Handlers
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result<string>>
    {
        private readonly IChangePasswordService _changePasswordService;
        private readonly ILogger<ChangePasswordCommandHandler> _logger;
        public ChangePasswordCommandHandler(IChangePasswordService changePasswordService, 
            ILogger<ChangePasswordCommandHandler> logger)
        {
            _changePasswordService = changePasswordService;
            _logger = logger;
        }
        public async Task<Result<string>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null || request.Request == null)
                {
                    return await Result<string>.FailAsync("Invalid request.");
                }
                var changePasswordData = request.Request;
                var result = await _changePasswordService.ChangePasswordAsync(
                    changePasswordData.UserId,
                    changePasswordData.CurrentPassword,
                    changePasswordData.NewPassword);

                return await Result<string>.SuccessAsync(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error changing password for user");
                return await Result<string>.FailAsync(ex.Message);
            }
        }
    }
}
