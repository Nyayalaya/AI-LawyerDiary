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
                // Validate input
                if (string.IsNullOrWhiteSpace(request.Email))
                {
                    _logger.LogWarning("Login attempt with empty email");
                    return Result<TokenResponse>.Fail("Email is required.");
                }

                if (string.IsNullOrWhiteSpace(request.Password))
                {
                    _logger.LogWarning("Login attempt with empty password");
                    return Result<TokenResponse>.Fail("Password is required.");
                }

                if (string.IsNullOrWhiteSpace(request.IpAddress))
                    request.IpAddress = "Unknown";

                _logger.LogInformation($"Login attempt for email: {request.Email}");

                // Map to TokenRequest
                var tokenRequest = new TokenRequest
                {
                    Email = request.Email,
                    Password = request.Password
                };

                // Call identity service
                var result = await _identityService.GetTokenAsync(tokenRequest, request.IpAddress);

                if (result.Succeeded)
                {
                    _logger.LogInformation($"User {request.Email} logged in successfully");
                }
                else
                {
                    _logger.LogWarning($"Failed login attempt for {request.Email}: {result.Message}");
                }

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
