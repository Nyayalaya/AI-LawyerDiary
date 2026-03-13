
using CourtApp.Application.Common;
using CourtApp.Application.DTOs.Common;
using MediatR;
using System;
using System.Collections.Generic;

namespace CourtApp.Application.Features.CourtType.Command
{
    public class CreateCourtTypeCommand : IRequest<Result<string>>
    {
        public string CourtType { get; set; }
        public string Abbreviation { get; set; }
        public List<LangDto> Language { get; set; }
    }
}
