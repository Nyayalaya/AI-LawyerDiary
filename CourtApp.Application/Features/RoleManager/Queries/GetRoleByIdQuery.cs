using CourtApp.Application.Common;
using MediatR;

namespace CourtApp.Application.Features.RoleManager.Queries
{
    public class GetRoleByIdQuery : IRequest<Result<RoleDetailResponse>>
    {
        public string Id { get; set; }
    }

    public class RoleDetailResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
