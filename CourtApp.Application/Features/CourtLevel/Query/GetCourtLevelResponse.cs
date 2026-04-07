using System;

namespace CourtApp.Application.Features.CourtLevel.Query
{
    public class GetCourtLevelResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
