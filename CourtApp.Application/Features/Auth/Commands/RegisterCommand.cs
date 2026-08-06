
using CourtApp.Application.Common;
using CourtApp.Application.Features.Auth.Dto;
using CourtApp.Domain.Enums;
using MediatR;

namespace CourtApp.Application.Features.Auth.Commands
{
    public class RegisterCommand : IRequest<Result<string>>
    {
        public RegisterType UserType { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public IndividualInfoDto IndividualInfoDto { get; set; }
        public CompanyInfoDto? CompanyInfoDto { get; set; }
        //public RegisterRequest RegistrationRequestData { get; set; }
    }
}