using System;

namespace CourtApp.Application.Features.WorkMaster.Dtos
{
    public class WorkMasterResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Guid CourtTypeId { get; set; }
    }
}
