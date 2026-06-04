using CourtApp.Application.Common;
using MediatR;
using System;

namespace CourtApp.Application.Features.CaseDetails.Commands
{
    public class CaseAssignedCommand : IRequest<Result<string>>
    {
        public Guid CaseId { get; set; }
        public string LawyerId { get; set; }
        public string UserId { get; set; }
    }
}
