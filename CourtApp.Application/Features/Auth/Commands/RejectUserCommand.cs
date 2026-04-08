using CourtApp.Application.Common;
using MediatR;

namespace CourtApp.Application.Features.Auth.Commands
{
    public class RejectUserCommand : IRequest<Result<string>>
    {
        public string UserId { get; set; }
    }
}
