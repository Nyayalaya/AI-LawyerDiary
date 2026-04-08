using CourtApp.Application.Common;
using CourtApp.Application.Features.Auth.Commands;
using CourtApp.Application.Features.Auth.Dto;
using CourtApp.Application.Features.Auth.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Auth.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<TokenResponse>>
    {
        private readonly IIdentityService _identityService;
        private readonly ILogger<LoginCommandHandler> _logger;

        public LoginCommandHandler(
            IIdentityService identityService,
            ILogger<LoginCommandHandler> logger)
        {
            _identityService = identityService;
            _logger = logger;
        }

        public async Task<Result<TokenResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var email = request.Email?.Trim().ToLower();
                var password = request.Password;

                var ipAddress = string.IsNullOrWhiteSpace(request.IpAddress) ?
                                    "Unknown"
                                    : request.IpAddress;
                _logger.LogInformation("Login attempt for Email: {Email}", email);

                var tokenRequest = new TokenRequest
                {
                    Email = request.Email,
                    Password = request.Password
                };
                var result = await _identityService.GetTokenAsync(tokenRequest, request.IpAddress);

                if (!result.Succeeded)
                    _logger.LogWarning($"Failed login attempt for {request.Email}: {result.Message}");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in LoginCommandHandler: {ex.Message}");
                return Result<TokenResponse>.Fail($"An error occurred during login: {ex.Message}");
            }
        }
    }
}
