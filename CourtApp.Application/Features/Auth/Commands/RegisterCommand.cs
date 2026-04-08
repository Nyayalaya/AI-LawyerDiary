
using CourtApp.Application.Common;
using CourtApp.Application.Features.Auth.Dto;
using MediatR;

namespace CourtApp.Application.Features.Auth.Commands
{
    public class RegisterCommand : IRequest<Result<string>>
    {
        public RegisterRequest RegistrationRequestData { get; set; }
    }
}