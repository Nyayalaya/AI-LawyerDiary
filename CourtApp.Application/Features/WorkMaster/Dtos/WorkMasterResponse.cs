using System;

namespace CourtApp.Application.Features.WorkMaster.Dtos
{
    public class WorkMasterResponse
    {
        public Guid Id { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public string Abbreviation { get; set; }
        public Guid CourtTypeId { get; set; }
    }
}
