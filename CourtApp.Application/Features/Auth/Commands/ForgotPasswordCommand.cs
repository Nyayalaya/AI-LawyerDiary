
using CourtApp.Application.Common;
using MediatR;


namespace CourtApp.Application.Features.Auth.Commands
{
    public class ForgotPasswordCommand : IRequest<Result<string>>
    {
        public string Email { get; set; }

        public string Origin { get; set; }
    }
}
