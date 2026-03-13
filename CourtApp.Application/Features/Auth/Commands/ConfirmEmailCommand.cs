
using CourtApp.Application.Common;
using MediatR;

namespace CourtApp.Application.Features.Auth.Commands
{
    public class ConfirmEmailCommand : IRequest<Result<string>>
    {
        public string UserId { get; set; }

        public string Code { get; set; }
    }
}
