using AuditTrail.Abstrations;
using CourtApp.Domain.Entities.LawyerDiary;
using CourtApp.Domain.Entities.Masters;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.CaseDetails
{

    [Table("case_against_data", Schema = "cases")]
    public class CaseAgainstEntity:AuditableEntity
    {
        public Guid CaseId { get; set; }
        public DateTime? ImpugedOrderDate { get; set; }
        public Guid CourtLevelId { get; set; }
        public Guid CourtTypeId { get; set; }
        public Guid? CourtDistrictId { get; set; }
        public Guid? CourtComplexId { get; set; }
        public Guid CourtId { get; set; }
        public Guid CourtHallId { get; set; }
        public Guid CaseCategoryId { get; set; }
        public Guid CaseTypeId { get; set; }
        public int StateId { get; set; }
        public string CaseNo { get; set; }
        public int CaseYear { get; set; }
        public string CisNumber { get; set; }
        public int? CisYear { get; set; }
        public string CnrNumber { get; set; }
        public string OfficerName { get; set; }
        public Guid? CadreId { get; set; }
        public virtual CaseEntity Case { get; set; }
        public virtual CourtLevelEntity CourtLevel { get; set; }
        public virtual CourtTypeEntity CourtType { get; set; }
        public virtual CourtDistrictEntity CourtDistrict { get; set; }
        public virtual CourtComplexEntity CourtComplex { get; set; }
        public virtual CourtEntity Court { get; set; }
        public virtual CourtHallEntity CourtHall { get; set; }
        public virtual CaseCategoryEntity CaseCategory { get; set; }
        public virtual TypeOfCasesEntity CaseType { get; set; }
        public virtual StateEntity State { get; set; }
        public virtual CadreEntity Cadre { get; set; }
    }
}
