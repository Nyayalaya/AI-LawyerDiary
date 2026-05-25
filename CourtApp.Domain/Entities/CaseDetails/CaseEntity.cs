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
        // =====================================================
        // BASIC CASE INFORMATION
        // =====================================================

        public string DiaryNumber { get; set; }

        public string CaseNo { get; set; }

        public int CaseYear { get; set; }

        public DateTime? FilingDate { get; set; }

        public DateTime? RegistrationDate { get; set; }

        public DateTime? NextDate { get; set; }

        public DateTime? DisposalDate { get; set; }

        public bool IsDisposed { get; set; }

        // =====================================================
        // TITLE INFORMATION
        // =====================================================

        public Guid FirstTitleId { get; set; }

        public Guid SecondTitleId { get; set; }

        public string CaseFirstTitle { get; set; }

        public string CaseSecondTitle { get; set; }

        // =====================================================
        // COURT HIERARCHY
        // =====================================================

        // Supreme / High / District / Tribunal

        public Guid CourtLevelId { get; set; }

        // Civil / Criminal / Family / Revenue etc.

        public Guid CourtTypeId { get; set; }

        // Only applicable for District Courts
        // Optional for some HC structures

        public Guid? CourtDistrictId { get; set; }

        // Court Campus / Complex
        // Optional for Supreme Court

        public Guid? CourtComplexId { get; set; }

        // Main Court

        public Guid CourtId { get; set; }

        // Court Hall / Bench / Room

        public Guid CourtHallId { get; set; }

        // =====================================================
        // CASE CLASSIFICATION
        // =====================================================

        public Guid CaseCategoryId { get; set; }
        public Guid? CaseStageId { get; set; }
        public Guid? CaseStatusId { get; set; }
        public Guid? PriorityId { get; set; }

        // =====================================================
        // LEGAL INFORMATION
        // =====================================================

        public string Act { get; set; }
        public string Section { get; set; }
        public string PoliceStation { get; set; }
        public string FIRNumber { get; set; }
        public int? FIRYear { get; set; }

        // =====================================================
        // CLIENT / PARTY INFORMATION
        // =====================================================

        public Guid? ClientId { get; set; }
        public Guid? OpponentClientId { get; set; }
       

        // =====================================================
        // FLAGS
        // =====================================================

        public bool IsImportant { get; set; }

        public bool IsArchived { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsUrgent { get; set; }

        public bool IsAssigned { get; set; }

        // =====================================================
        // REMARKS
        // =====================================================

        public string Remarks { get; set; }
        public string InternalRemarks { get; set; }

        // =====================================================
        // SELF REFERENCING
        // =====================================================

        public Guid? ParentCaseId { get; set; }

        // =====================================================
        // NAVIGATION PROPERTIES
        // =====================================================

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

        // =====================================================
        // COLLECTIONS
        // =====================================================

        public virtual ICollection<AssignCaseEntity>  CaseAssignedEntities { get; set; }

        public virtual ICollection<CaseProcedingEntity>  CaseProceedingEntities { get; set; }

        public virtual ICollection<CaseDocsEntity> CaseDocumentEntities { get; set; }

        public virtual ICollection<CaseEntity>  ChildCases { get; set; }
    }
}