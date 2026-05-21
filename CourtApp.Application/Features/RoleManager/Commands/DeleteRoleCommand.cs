using CourtApp.Application.Common;
using MediatR;

namespace CourtApp.Application.Features.RoleManager.Commands
{
    public class DeleteRoleCommand : IRequest<Result<bool>>
    {
        public string Id { get; set; }
    }
}
