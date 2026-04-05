using System;

namespace CourtApp.Application.Features.CaseStages.Query
{
    public class CaseStageQueryByIdResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
