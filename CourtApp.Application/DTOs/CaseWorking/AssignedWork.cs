using System;

namespace CourtApp.Application.DTOs.CaseWorking
{
    public class AssignedWork
    {
        public Guid Id { get; set; }
        public Guid WorkId { get; set; }        
        public string WorkDetail { get; set; }
    }
}
