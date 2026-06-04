using AuditTrail.Abstrations;
using CourtApp.Domain.Entities.LawyerDiary;
using CourtApp.Domain.Entities.Masters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.CaseDetails
{
    [Table("case_detail_data")]
    public class CaseEntity : AuditableEntity
    {
        public string DiaryNumber { get; set; }
        public string CaseNo { get; set; }
        public int CaseYear { get; set; }
        public DateTime InstitutionDate { get; set; }
        public DateTime? NextDate { get; set; }
        public DateTime? DisposalDate { get; set; }
        public bool IsDisposed { get; set; }
        public Guid FirstTitleId { get; set; }
        public Guid SecondTitleId { get; set; }
        public string CaseFirstTitle { get; set; }
        public string CaseSecondTitle { get; set; }
        public Guid CourtLevelId { get; set; }
        public Guid CourtTypeId { get; set; }
        public Guid? CourtDistrictId { get; set; }
        public Guid? CourtComplexId { get; set; }
        public Guid CourtId { get; set; }
        public Guid CourtHallId { get; set; }
        public Guid CaseCategoryId { get; set; }
        public Guid? CaseStageId { get; set; }
        public Guid? CaseStatusId { get; set; }
        public Guid? PriorityId { get; set; }
        public string Act { get; set; }
        public string Section { get; set; }
        public string PoliceStation { get; set; }
        public string FIRNumber { get; set; }
        public int? FIRYear { get; set; }
        public Guid? ClientId { get; set; }
        public Guid? OpponentClientId { get; set; }
        public bool IsImportant { get; set; }
        public bool IsArchived { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsUrgent { get; set; }
        public bool IsAssigned { get; set; }
        public string Remarks { get; set; }
        public string InternalRemarks { get; set; }
        public Guid? ParentCaseId { get; set; }

        [ForeignKey(nameof(FirstTitleId))]
        public virtual FSTitleEntity FirstTitle { get; set; }

        [ForeignKey(nameof(SecondTitleId))]
        public virtual FSTitleEntity SecondTitle { get; set; }

        [ForeignKey(nameof(CourtLevelId))]
        public virtual CourtLevelEntity CourtLevel { get; set; }

        [ForeignKey(nameof(CourtTypeId))]
        public virtual CourtTypeEntity CourtType { get; set; }

        [ForeignKey(nameof(CourtDistrictId))]
        public virtual CourtDistrictEntity CourtDistrict { get; set; }

        [ForeignKey(nameof(CourtComplexId))]
        public virtual CourtComplexEntity CourtComplex { get; set; }

        [ForeignKey(nameof(CourtId))]
        public virtual CourtEntity Court { get; set; }

        [ForeignKey(nameof(CourtHallId))]
        public virtual CourtHallEntity CourtHall { get; set; }

        [ForeignKey(nameof(CaseCategoryId))]
        public virtual CaseCategoryEntity CaseCategory { get; set; }

        [ForeignKey(nameof(CaseStageId))]
        public virtual CaseStageEntity CaseStage { get; set; }

        [ForeignKey(nameof(ClientId))]
        public virtual ClientEntity Client { get; set; }

        [ForeignKey(nameof(OpponentClientId))]
        public virtual ClientEntity OpponentClient { get; set; }

        [ForeignKey(nameof(ParentCaseId))]
        public virtual CaseDetailEntity ParentCase { get; set; }
        public virtual ICollection<CaseAgainstEntity> CaseAgainstEntities { get; set; }
        public virtual ICollection<CaseAssignedEntity> CaseAssignedEntities { get; set; }
        public virtual ICollection<CaseProcedingEntity> CaseProceedingEntities { get; set; }
        public virtual ICollection<CaseDocsEntity> CaseDocumentEntities { get; set; }
        public virtual ICollection<CaseEntity> ChildCases { get; set; }
    }
}