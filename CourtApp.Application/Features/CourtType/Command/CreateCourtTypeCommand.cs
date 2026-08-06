
using CourtApp.Application.Common;
using CourtApp.Application.DTOs.Common;
using MediatR;
using System;
using System.Collections.Generic;

namespace CourtApp.Application.Features.CourtType.Command
{
    public class CreateCourtTypeCommand : IRequest<Result<string>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public List<LangDto> Language { get; set; }
    }
}
