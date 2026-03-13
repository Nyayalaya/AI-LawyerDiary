
using CourtApp.Application.Common;
using CourtApp.Application.Features.Auth.Commands;
using CourtApp.Application.Features.Auth.Dto;
using CourtApp.Application.Features.Auth.Services;
using CourtApp.Application.Interfaces.Shared;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Auth.Handlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<string>>
    {
        private readonly IIdentityService _identityService;
        private readonly ICurrentRequestProvider _currentRequestProvider;

        public RegisterCommandHandler(
            IIdentityService identityService,
            ICurrentRequestProvider currentRequestProvider)
        {
            _identityService = identityService;
            _currentRequestProvider = currentRequestProvider;
        }

        public async Task<Result<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            // Build RegisterRequest from RegisterCommand
            var registrationRequest = new RegisterRequest
            {
                UserType = request.UserType,
                Email = request.Email,
                Password = request.Password,
                Contact = request.Contact,
                IndividualInfoDto = request.IndividualInfoDto,
                CompanyInfoDto = request.CompanyInfoDto,
                Origin = _currentRequestProvider.GetOrigin()
            };

            // Call identity service to register
            var result = await _identityService.RegisterAsync(registrationRequest);

            return result;
        }
    }
}