using CourtApp.Application.Common;
using CourtApp.Application.Features.Auth.Dto;
using MediatR;
namespace CourtApp.Application.Features.Auth.Commands
{
    public class ChangePasswordCommand:IRequest<Result<string>>
    {
        public ChangePasswordRequest Request { get; set; }
    }
}
