using CourtApp.Application.Common;
using CourtApp.Application.Features.CaseDetails.Dtos;
using MediatR;

namespace CourtApp.Application.Features.CaseDetails.Commands
{
    public class CreateCaseCommand : IRequest<Result<string>>
    {
        public CaseRequestDto Case { get; set; }
    }
}
