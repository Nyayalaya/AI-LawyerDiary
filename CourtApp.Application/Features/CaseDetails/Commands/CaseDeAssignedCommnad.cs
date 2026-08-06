using CourtApp.Application.Common;
using MediatR;
using System;

namespace CourtApp.Application.Features.CaseDetails.Commands
{
    public class CaseDeAssignedCommnad : IRequest<Result<string>>
    {
        public Guid CaseId { get; set; }
        public string LawyerId { get; set; }
        public string Remark { get; set; }
    }
}
