using CourtApp.Application.DTOs.Common;
using System;
using System.Collections.Generic;

namespace CourtApp.Application.Features.CourtType.Query
{
    public class GetCourtTypeResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public List<LangDto> Language { get; set; }
    }
}
