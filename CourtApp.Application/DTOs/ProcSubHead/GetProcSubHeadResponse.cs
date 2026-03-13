using System;

namespace CourtApp.Application.DTOs.ProcSubHead
{
    public class GetProcSubHeadResponse
    {
        public Guid Id { get; set; }
        public string Head { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
    }
}
