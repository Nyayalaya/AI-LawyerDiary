using System;

namespace CourtApp.Application.DTOs.ProceedingHead
{
    public class GetProceedingHeadResponse
    {
        public Guid Id { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public string Abbreviation { get; set; }
    }
}
