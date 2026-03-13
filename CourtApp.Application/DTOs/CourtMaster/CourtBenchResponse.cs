using System;

namespace CourtApp.Application.DTOs.CourtMaster
{
    public class CourtBenchResponse
    {
        public Guid Id { get; set; }
        public string Abbreviation { get; set; }
        public string CourtBench_En { get; set; }
        public string CourtBench_Hn { get; set; }
        public string Address { get; set; }
    }
}
