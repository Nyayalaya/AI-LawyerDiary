using System;

namespace CourtApp.Application.DTOs.CourtMaster
{
    public class GetCourtMasterDataAllResponse
    {
        public Guid Id { get; set; }
        public string CourtType { get; set; }
        public string CourtName { get; set; }
        public string CourtFullName { get; set; }
        public string Bench { get; set; }
        public string HeadQuerter { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public string Complex { get; set; }
        public int Total { get; set; }
    }
}
