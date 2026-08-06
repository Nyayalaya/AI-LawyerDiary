using CourtApp.Application.Common;
using CourtApp.Application.Features.Auth.Commands;
using CourtApp.Application.Features.Auth.Dto;
using CourtApp.Application.Features.Auth.Services;
using CourtApp.Application.Interfaces.Shared;
using CourtApp.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<string>>
{
    private readonly ICurrentRequestProvider _currentRequestProvider;
    private readonly IUserBasicInfoService _userBasicInfoService;
    private readonly IRegistrationService _registrationService;
    private readonly ILogger<RegisterCommandHandler> _logger;

    public RegisterCommandHandler(
        IUserBasicInfoService userBasicInfoService,
        ICurrentRequestProvider currentRequestProvider,
        IRegistrationService registrationService,
        ILogger<RegisterCommandHandler> logger,
        IMailService mailService)
    {
        _userBasicInfoService = userBasicInfoService;
        _currentRequestProvider = currentRequestProvider;
        _registrationService = registrationService;
        _logger = logger;
    }

    public async Task<Result<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var data = request;
            if (data == null)
                return await Result<string>.FailAsync("Invalid request");

            
            if (await _userBasicInfoService.IsEmailExistAsync(data.Email))
                return await Result<string>.FailAsync("Email is already taken.");

            if (await _userBasicInfoService.IsContactExistAsync(data.Contact))
                return await Result<string>.FailAsync("Contact number is already taken.");

            
            if (data.UserType == RegisterType.Lawyer)
            {
                if (await _userBasicInfoService.IsEnrollmentExistAsync(data.IndividualInfoDto.EnrollmentNumber))
                    return await Result<string>.FailAsync("Enrollment number is already taken.");
            }

            
            if (data.UserType == RegisterType.Corporate)
            {
                if (await _userBasicInfoService.IsRegistrationNumberExistAsync(data.CompanyInfoDto.RegistrationNumber))
                    return await Result<string>.FailAsync("Registration number is already taken.");
            }

            string userId;
            try
            {
                var registerRequestInfo = new RegisterRequest
                {
                    Email = data.Email,
                    Contact = data.Contact,
                    UserType = data.UserType,
                    IndividualInfoDto = data.IndividualInfoDto,
                    CompanyInfoDto = data.CompanyInfoDto,
                    Password=data.Password
                };
                userId = await _registrationService.RegisterAsync(registerRequestInfo);
            }
            catch (Exception ex)
            {
                return await Result<string>.FailAsync(ex.Message);
            }

            _logger.LogInformation("User registered successfully. UserId: {UserId}, Email: {Email}", userId, data.Email);

            return await Result<string>.SuccessAsync("User registered successfully. Awaiting approval.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred during user registration for Email: {Email}", request?.Email);

            return await Result<string>.FailAsync(ex.Message);
        }
    }

    

}
