using AuditTrail.Abstrations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{
    [Table("r_court_bench", Schema = "ld")]
    public class CourtBenchEntity 
    {
        public  Guid Id { get; set; }
        public Guid CourtMasterId { get; set; }
        public string CourtBench_En { get; set; }
        public string CourtBench_Hn { get; set; }
        public string Abbreviation { get; set; }
        public string Address { get; set; }
        public virtual CourtMasterEntity CourtMaster { get; set; }
    }
}
