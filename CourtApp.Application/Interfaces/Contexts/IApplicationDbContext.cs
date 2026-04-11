using CourtApp.Domain.Entities.Account;
using CourtApp.Domain.Entities.AI;
using CourtApp.Domain.Entities.CaseDetails;
using CourtApp.Domain.Entities.Common;
using CourtApp.Domain.Entities.FormBuilder;
using CourtApp.Domain.Entities.LawyerDiary;
using CourtApp.Domain.Entities.Masters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Contexts
{
    public interface IApplicationDbContext
    {
        IDbConnection Connection { get; }
        bool HasChanges { get; }

        EntityEntry Entry(object entity);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        #region 🔹 MASTER
        DbSet<StateEntity> States { get; set; }
        DbSet<DistrictEntity> Districts { get; set; }
        DbSet<CityEntity> Cities { get; set; }
        DbSet<LocationEntity> Locations { get; set; }

        DbSet<CourtTypeEntity> CourtTypes { get; set; }
        DbSet<CourtLevelEntity> CourtLevels { get; set; }
        DbSet<CourtDistrictEntity> CourtDistricts { get; set; }
        DbSet<CourtComplexEntity> CourtComplexes { get; set; }
        DbSet<CourtEntity> Courts { get; set; }
        DbSet<CourtHallEntity> CourtHalls { get; set; }
        DbSet<CourtBenchEntity> CourtBenches { get; set; }
        DbSet<JudgeEntity> Judges { get; set; }

        DbSet<LanguageEntity> Languages { get; set; }
        DbSet<MultiLangDictEntity> MultiLangDicts { get; set; }

        DbSet<FormTypeEntity> FormTypes { get; set; }
        DbSet<FormMasterEntity> FormMasters { get; set; }
        DbSet<FormSubtypeEntity> FormSubtypes { get; set; }
        DbSet<FormTemplateEntity> FormTemplates { get; set; }
        DbSet<FormTemplateVersionEntity> FormTemplateVersions { get; set; }
        DbSet<FormCaseCategoryMapping> FormCaseTypeMappings { get; set; }
        DbSet<FormCourtMapping> FormCourtMappings { get; set; }

        #endregion

        #region 🔹 CASE MODULE
        DbSet<CaseDetailEntity> Cases { get; set; }
        DbSet<CaseDetailAgainstEntity> AgainstCases { get; set; }
        DbSet<AssignCaseEntity> AssignedCases { get; set; }

        DbSet<CaseStageEntity> CaseStages { get; set; }
        DbSet<CaseCategoryEntity> CaseCategories { get; set; }
        DbSet<TypeOfCasesEntity> CaseTypes { get; set; }
        DbSet<CaseKindEntity> CaseKinds { get; set; }
        DbSet<CaseTitleEntity> CaseTitles { get; set; }

        DbSet<CaseProcedingEntity> CaseProceedings { get; set; }
        DbSet<CaseWorkEntity> CaseWorks { get; set; }
        DbSet<CaseDocsEntity> CaseDocuments { get; set; }

        DbSet<DOTypeEntity> DOTypes { get; set; }
        DbSet<FSTitleEntity> FSTitles { get; set; }
        #endregion

        #region 🔹 LAWYER DIARY
        DbSet<ClientEntity> Clients { get; set; }
        DbSet<LawyerMasterEntity> Lawyers { get; set; }

        DbSet<LDBookEntity> LDBooks { get; set; }
        DbSet<BookTypeEntity> BookTypes { get; set; }
        DbSet<PublisherEntity> Publishers { get; set; }

        DbSet<ExpenseHeadEntity> ExpenseHeads { get; set; }
        DbSet<BillingDetailEntity> BillingDetails { get; set; }

        DbSet<CadreMasterEntity> Cadres { get; set; }
        DbSet<Specialization> Specializations { get; set; }
        #endregion

        #region 🔹 WORK & PROCEEDING
        DbSet<WorkTypeEntity> WorkTypes { get; set; }
        DbSet<WorksEntity> Works { get; set; }

        DbSet<ProceedingHeadEntity> ProceedingHeads { get; set; }
        DbSet<ProceedingSubHeadEntity> ProceedingSubHeads { get; set; }
        #endregion

        #region 🔹 FORM BUILDER
        DbSet<FormBuilderEntity> FormBuilders { get; set; }
        DbSet<FormTemplateMappingEntity> FormTemplateMappings { get; set; }
        DbSet<DraftingDetailEntity> DraftingDetails { get; set; }
        #endregion

        #region 🔹 AI MODULE
        DbSet<AIConversation> AIConversations { get; set; }
        DbSet<DocumentChunk> DocumentChunks { get; set; }
        DbSet<DocumentChunkEmbedding> ChunkEmbeddings { get; set; }
        DbSet<LegalCitationEntity> LegalCitations { get; set; }
        #endregion
    }
}
