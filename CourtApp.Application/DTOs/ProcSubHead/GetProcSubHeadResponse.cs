using System;

namespace CourtApp.Application.DTOs.ProcSubHead
{
    public class GetProcSubHeadResponse
    {
        public Guid Id { get; set; }
        public string ProceedingType { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
