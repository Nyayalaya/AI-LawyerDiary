using System;

namespace CourtApp.Application.DTOs.Cadre
{
    public class GetCadreResponseById
    {
        public Guid Id { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
    }
}
