
using CourtApp.Application.Common;
using MediatR;


namespace CourtApp.Application.Features.Auth.Commands
{
    public class ResetPasswordCommand : IRequest<Result<string>>
    {
        public string Email { get; set; }

        public string Token { get; set; }

        public string Password { get; set; }
    }
}
