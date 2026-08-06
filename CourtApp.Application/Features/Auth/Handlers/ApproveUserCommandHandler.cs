using CourtApp.Application.Common;
using CourtApp.Application.Features.Auth.Commands;
using CourtApp.Application.Features.Auth.Services;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Auth.Handlers
{
    public class ApproveUserCommandHandler : IRequestHandler<ApproveUserCommand, Result<string>>
    {
        private readonly IUserApprovalService _approvalService;

        public ApproveUserCommandHandler(IUserApprovalService approvalService)
        {
            _approvalService = approvalService;
        }

        public async Task<Result<string>> Handle(ApproveUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var message = await _approvalService.ApproveUserAsync(request.UserId);
                return await Result<string>.SuccessAsync(message);
            }
            catch (Exception ex)
            {
                return await Result<string>.FailAsync(ex.Message);
            }
        }
    }
}
