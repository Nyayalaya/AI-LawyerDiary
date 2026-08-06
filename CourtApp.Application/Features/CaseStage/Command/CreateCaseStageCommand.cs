using CourtApp.Application.Common;
using CourtApp.Application.DTOs.Common;
using MediatR;
using System.Collections.Generic;

namespace CourtApp.Application.Features.CaseStages.Command
{
    public class CreateCaseStageCommand : IRequest<Result<string>>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public List<LangDto> Language { get; set; }
    }
}
