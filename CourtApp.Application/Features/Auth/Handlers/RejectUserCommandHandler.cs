using CourtApp.Application.Common;
using CourtApp.Application.Features.Auth.Commands;
using CourtApp.Application.Features.Auth.Services;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Auth.Handlers
{
    public class RejectUserCommandHandler:IRequestHandler<RejectUserCommand,Result<string>>
    {
        private readonly IUserApprovalService _approvalService;

        public RejectUserCommandHandler(IUserApprovalService approvalService)
        {
            _approvalService = approvalService;
        }

        public async Task<Result<string>> Handle(RejectUserCommand request, CancellationToken cancellationToken)
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
