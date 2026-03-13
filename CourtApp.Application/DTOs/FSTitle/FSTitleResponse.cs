using System;

namespace CourtApp.Application.DTOs.FSTitle
{
    public class FSTitleResponse
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
    }
}
