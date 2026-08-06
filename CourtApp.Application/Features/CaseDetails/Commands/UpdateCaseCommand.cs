using CourtApp.Application.Common;
using CourtApp.Application.Features.CaseDetails.Dtos;
using MediatR;
using System;

namespace CourtApp.Application.Features.CaseDetails.Commands
{
    public class UpdateCaseCommand : IRequest<Result<string>>
    {
        public Guid Id { get; set; }
        public CaseRequestDto Case { get; set; }
    }
}
