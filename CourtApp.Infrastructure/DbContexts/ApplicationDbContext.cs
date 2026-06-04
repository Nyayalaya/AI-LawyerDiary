using AuditTrail.Abstrations;
using CourtApp.Application.Interfaces.Contexts;
using CourtApp.Application.Interfaces.Shared;
using CourtApp.Domain.Entities.Account;
using CourtApp.Domain.Entities.AI;
using CourtApp.Domain.Entities.CaseDetails;
using CourtApp.Domain.Entities.Common;
using CourtApp.Domain.Entities.FormBuilder;
using CourtApp.Domain.Entities.LawyerDiary;
using CourtApp.Domain.Entities.Masters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.DbContexts
{
    public class ApplicationDbContext : AuditableContext, IApplicationDbContext
    {
        private readonly IDateTimeService _dateTime;
        private readonly IAuthenticatedUserService _authenticatedUser;

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            IDateTimeService dateTime,
            IAuthenticatedUserService authenticatedUser) : base(options)
        {
            _dateTime = dateTime;
            _authenticatedUser = authenticatedUser;

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            ChangeTracker.AutoDetectChangesEnabled = false; // 🚀 performance
        }

        public IDbConnection Connection => Database.GetDbConnection();
        public bool HasChanges => ChangeTracker.HasChanges();

        #region 🔹 MASTER TABLES
        public DbSet<StateEntity> States { get; set; }
        public DbSet<DistrictEntity> Districts { get; set; }
        public DbSet<CityEntity> Cities { get; set; }
        public DbSet<CourtTypeEntity> CourtTypes { get; set; }
        public DbSet<CourtLevelEntity> CourtLevels { get; set; }
        public DbSet<CourtDistrictEntity> CourtDistricts { get; set; }
        public DbSet<CourtComplexEntity> CourtComplexes { get; set; }
        public DbSet<CourtEntity> Courts { get; set; }
        public DbSet<CourtHallEntity> CourtHalls { get; set; }
        public DbSet<CourtBenchEntity> CourtBenches { get; set; }
        public DbSet<JudgeEntity> Judges { get; set; }
        public DbSet<LocationEntity> Locations { get; set; }
        public DbSet<LanguageEntity> Languages { get; set; }
        public DbSet<MultiLangDictEntity> MultiLangDicts { get; set; }
        public DbSet<CadreMasterEntity> Cadres { get; set; }
        public DbSet<CourtFormTypeEntity> CourtFormTypes { get; set; }
        public DbSet<WorksEntity> Works { get; set; }
        public DbSet<WorkTypeEntity> WorkTypes { get; set; }
        public DbSet<WorksEntity> WorkSubTypes { get; set; }
        public DbSet<ProceedingTypeEntity> ProceedingTypes { get; set; }
        public DbSet<ProceedingEntity> Proceedings { get; set; }
        public DbSet<FormTypeEntity> FormTypes { get; set; }
        public DbSet<FormMasterEntity> FormMasters { get; set; }
        public DbSet<FormSubtypeEntity> FormSubtypes { get; set; }
        public DbSet<FormTemplateEntity> FormTemplates { get; set; }
        public DbSet<FormTemplateVersionEntity> FormTemplateVersions { get; set; }
        public DbSet<FormCaseCategoryMapping> FormCaseTypeMappings { get; set; }
        public DbSet<FormCourtMapping> FormCourtMappings { get; set; }

        #endregion

        #region 🔹 CASE MODULE
        public DbSet<CaseDetailEntity> Cases { get; set; }
        public DbSet<CaseAgainstEntity> AgainstCases { get; set; }
        public DbSet<CaseStageEntity> CaseStages { get; set; }
        public DbSet<CaseCategoryEntity> CaseCategories { get; set; }
        public DbSet<TypeOfCasesEntity> CaseTypes { get; set; }
        public DbSet<CaseKindEntity> CaseKinds { get; set; }
        public DbSet<CaseTitleEntity> CaseTitles { get; set; }
        public DbSet<CaseProcedingEntity> CaseProceedings { get; set; }
        public DbSet<CaseWorkEntity> CaseWorks { get; set; }
        public DbSet<CaseDocsEntity> CaseDocuments { get; set; }
        public DbSet<CaseAssignedEntity> AssignedCases { get; set; }
        public DbSet<DOTypeEntity> DOTypes { get; set; }
        public DbSet<CaseEntity> CasesData { get; set; }
        #endregion

        #region 🔹 LAWYER DIARY
        public DbSet<ClientEntity> Clients { get; set; }
        public DbSet<LawyerMasterEntity> Lawyers { get; set; }
        public DbSet<LDBookEntity> LDBooks { get; set; }
        public DbSet<BookTypeEntity> BookTypes { get; set; }
        public DbSet<PublisherEntity> Publishers { get; set; }
        public DbSet<SubjectEntity> PracticeSubjects { get; set; }
        public DbSet<ExpenseHeadEntity> ExpenseHeads { get; set; }
        public DbSet<BillingDetailEntity> BillingDetails { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        #endregion

        #region 🔹 COURT FEES & WORK
        public DbSet<CourtFeeEntity> CourtFees { get; set; }
        public DbSet<CourtFeeTypeEntity> CourtFeeTypes { get; set; }
        public DbSet<CourtFeeStructureEntity> CourtFeeStructures { get; set; }
        
        public DbSet<CourtMasterEntity> CourtMasters { get; set; }
        #endregion

        #region 🔹 FORM BUILDER
        public DbSet<FormBuilderEntity> FormBuilders { get; set; }
        public DbSet<FormTemplateMappingEntity> FormTemplateMappings { get; set; }
        public DbSet<DraftingDetailEntity> DraftingDetails { get; set; }
        public DbSet<FSTitleEntity> FSTitles { get; set; }
        public DbSet<TemplateInfoEntity> TemplateInfos { get; set; }
        #endregion

        #region 🔹 AI MODULE
        public DbSet<AIConversation> AIConversations { get; set; }
        public DbSet<DocumentChunk> DocumentChunks { get; set; }
        public DbSet<DocumentChunkEmbedding> ChunkEmbeddings { get; set; }
        public DbSet<LegalCitationEntity> LegalCitations { get; set; }
         #endregion

        #region 🔹 AUDIT
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ChangeTracker.DetectChanges();

            var userId = _authenticatedUser?.UserId ?? "SYSTEM";

            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedOn = _dateTime.NowUtc;
                    entry.Entity.CreatedBy = userId;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.LastModifiedOn = _dateTime.NowUtc;
                    entry.Entity.LastModifiedBy = userId;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
        #endregion

        #region 🔹 MODEL CONFIG
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // ✅ Decimal precision
            foreach (var property in builder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,2)");
            }

            builder.Entity<ClientEntity>()
                .Property(c => c.ClientType)
                .HasConversion<string>();

           

            // 🔗 CourtMaster relations
            builder.Entity<CourtMasterEntity>()
                .HasOne(e => e.CourtComplex)
                .WithMany()
                .HasForeignKey(e => e.CourtComplexId)
                .IsRequired(false);

            builder.Entity<CourtMasterEntity>()
                .HasOne(e => e.CourtDistrict)
                .WithMany()
                .HasForeignKey(e => e.CourtDistrictId)
                .IsRequired(false);

            #region JSON CONFIG

            builder.Ignore<FieldSizeEntity>();

            builder.Entity<FormBuilderEntity>().OwnsOne(f => f.FieldsDetails, d =>
            {
                d.ToJson();
                d.OwnsMany(x => x.Fields).OwnsOne(x => x.FieldSize);
            });

            builder.Entity<DraftingDetailEntity>().OwnsMany(x => x.FieldDetails, j => j.ToJson());
            builder.Entity<TemplateInfoEntity>().OwnsMany(x => x.Tags, j => j.ToJson());
            builder.Entity<FormTemplateMappingEntity>().OwnsMany(x => x.FieldsMapping, j => j.ToJson());
            builder.Entity<CaseTitleEntity>().OwnsMany(x => x.CaseApplicants, j => j.ToJson());

            builder.Entity<CaseProcedingEntity>().OwnsOne(x => x.ProcWork, j =>
            {
                j.ToJson();
                j.OwnsMany(x => x.Works);
            });

            // Multi-language
            builder.Entity<LanguageEntity>().OwnsMany(x => x.Languages, j => j.ToJson());
            builder.Entity<StateEntity>().OwnsMany(x => x.Languages, j => j.ToJson());
            builder.Entity<CourtEntity>().OwnsMany(x => x.Languages, j => j.ToJson());
            builder.Entity<LocationEntity>().OwnsMany(x => x.Languages, j => j.ToJson());
            builder.Entity<MultiLangDictEntity>().OwnsMany(x => x.MultiLangs, j => j.ToJson());
            builder.Entity<CourtDistrictEntity>().OwnsMany(x => x.Languages, j => j.ToJson());
            builder.Entity<CaseCategoryEntity>().OwnsMany(x => x.Languages, j => j.ToJson());
            builder.Entity<CaseStageEntity>().OwnsMany(x => x.Languages, j => j.ToJson());
            builder.Entity<CourtComplexEntity>().OwnsMany(x => x.Languages, j => j.ToJson());
            builder.Entity<CourtHallEntity>().OwnsMany(x => x.Languages, j => j.ToJson());
            builder.Entity<CourtTypeEntity>().OwnsMany(x => x.Languages, j => j.ToJson());


            #endregion

            // ✅ PG VECTOR
            builder.HasPostgresExtension("vector");

            builder.Entity<DocumentChunkEmbedding>()
                .Property(x => x.Embedding)
                .HasColumnType("vector(1536)");

            builder.Entity<DocumentChunkEmbedding>()
                .HasIndex(x => x.Embedding)
                .HasMethod("hnsw")
                .HasOperators("vector_cosine_ops");

            builder.Entity<DocumentChunk>()
                .HasIndex(x => new { x.DocumentId, x.PageNumber });
        }
        #endregion
    }
}
