using CourtApp.Application.Common;
using CourtApp.Application.Features.Profile.Commands;
using CourtApp.Application.Features.Profile.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Profile.Handlers
{
    public class CreateSubUserCommandHandler : IRequestHandler<CreateSubUserCommand, Result<string>>
    {
        private readonly IUserHierarchyService _userHierarchyService;

        public CreateSubUserCommandHandler(IUserHierarchyService userHierarchyService)
        {
            _userHierarchyService = userHierarchyService;
        }

        public async Task<Result<string>> Handle(CreateSubUserCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.ParentUserId))
                return Result<string>.Fail("Parent User ID is required");

            if (request.SubUser == null)
                return Result<string>.Fail("Sub-user data is required");

            var result = await _userHierarchyService.CreateSubUserAsync(request.SubUser, request.ParentUserId);
            return await Result<string>.SuccessAsync("Sub-user create successfully!");
        }
    }
}
