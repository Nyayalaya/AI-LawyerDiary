using System;
namespace CourtApp.Application.DTOs.WorkSub
{
    public class WorkSubMasterByIdResponse
    {
        public  Guid Id { get; set; }
        public Guid WorkId { get; set; }
        public required string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public string Abbreviation { get; set; }
    }
}
