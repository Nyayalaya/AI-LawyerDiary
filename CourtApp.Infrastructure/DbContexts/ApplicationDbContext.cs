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
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.DbContexts
{
    public class ApplicationDbContext : AuditableContext, IApplicationDbContext
    {
        private readonly IDateTimeService _dateTime;
        private readonly IAuthenticatedUserService _authenticatedUser;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
            IDateTimeService dateTime,
            IAuthenticatedUserService authenticatedUser) : base(options)
        {
            _dateTime = dateTime;
            _authenticatedUser = authenticatedUser;
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public IDbConnection Connection => Database.GetDbConnection();
        public bool HasChanges => ChangeTracker.HasChanges();
        public DbSet<StateEntity> States { get; set; }
        public DbSet<DistrictEntity> Districts { get; set; }
        public DbSet<CityEntity> CityEntities { get; set; }
        public DbSet<BookTypeEntity> BookTypes { get; set; }
        public DbSet<CaseCategoryEntity> CaseCategories { get; set; }
        public DbSet<LDBookEntity> LDBooks { get; set; }
        public DbSet<PublisherEntity> Publishers { get; set; }
        public DbSet<SubjectEntity> PracticeSubjects { get; set; }
        public DbSet<ExpenseHeadEntity> ExpenseHeads { get; set; }
        public DbSet<CourtFeeEntity> CourtFees { get; set; }
        public DbSet<CourtFeeTypeEntity> CourtFeeTypes { get; set; }
        public DbSet<CaseKindEntity> CaseKinds { get; set; }
        public DbSet<TypeOfCasesEntity> Typeofcases { get; set; }
        public DbSet<CaseStageEntity> CaseStages { get; set; }
        public DbSet<CourtMasterEntity> CourtMasters { get; set; }
        public DbSet<CourtFeeStructureEntity> CourtFeeStructures { get; set; }
        public DbSet<ClientEntity> Clients { get; set; }
        public DbSet<CourtTypeEntity> CourtTypes { get; set; }
        public DbSet<CaseDetailEntity> Cases { get; set; }
        public DbSet<CaseDetailAgainstEntity> AgainstCaseDetails { get; set; }
        public DbSet<LawyerMasterEntity> Laywers { get; set; }
        public DbSet<ProceedingHeadEntity> ProceedingHeads { get; set; }
        public DbSet<ProceedingSubHeadEntity> ProceedingSubHeads { get; set; }
        public DbSet<WorkMasterEntity> WorkMasters { get; set; }
        public DbSet<WorkMasterSubEntity> WorkMasterSubs { get; set; }
        public DbSet<CourtDistrictEntity> CDistricts { get; set; }
        public DbSet<CourtComplexEntity> CourtComplex { get; set; }
        public DbSet<CaseTitleEntity> CaseTitiles { get; set; }
        public DbSet<CourtBenchEntity> CourtBenchEntities { get; set; }
        public DbSet<CaseProcedingEntity> CaseProcedingEntities { get; set; }
        public DbSet<CaseWorkEntity> CaseWorkEntities { get; set; }
        public DbSet<DOTypeEntity> DOTypeEntities { get; set; }
        public DbSet<CaseDocsEntity> caseDocsEntities { get; set; }
        public DbSet<FSTitleEntity> FSTitleEntities { get; set; }
        public DbSet<FormBuilderEntity> DynamicFrmBuilders { get; set; }
        public DbSet<DraftingDetailEntity> CaseTempMappings { get; set; }
        public DbSet<FormTemplateMappingEntity> TempFormMappings { get; set; }
        public DbSet<CadreMasterEntity> Cadres { get; set; }
        public DbSet<SpecializationEntity> Specilities { get; set; }
        public DbSet<AssignCaseEntity> AssignedCases { get; set; }
        public DbSet<LanguageEntity> LanguageEntities { get; set; }
        public DbSet<CourtFormTypeEntity> CourtFormTypeEntities { get; set; }
        public DbSet<BillingDetailEntity> BillingDetails { get; set; }
        public DbSet<MultiLangDictEntity> MultiLangDictEntities { get; set; }
        public DbSet<CourtLevelEntity> courtLevelEntities { get; set; }

        public DbSet<AIConversation> AIConversations { get; set; }
        public DbSet<DocumentChunk> DocumentChunks { get; set; }
        public DbSet<LegalCitationEntity> LegalCitations { get; set; }
        public DbSet<DocumentChunkEmbedding> ChunkEmbeddings { get; set; }
        public bool SkipAudit { get; set; } = false;

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            ChangeTracker.DetectChanges();

            if (!SkipAudit)
            {
                var userId = _authenticatedUser?.UserId ?? Guid.Empty.ToString();
                foreach (var entry in ChangeTracker.Entries<AuditableEntity>().ToList())
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.Entity.CreatedOn = _dateTime.NowUtc;
                            entry.Entity.CreatedBy = userId;
                            break;

                        case EntityState.Modified:
                            entry.Entity.LastModifiedOn ??= _dateTime.NowUtc;
                            entry.Entity.LastModifiedBy = _authenticatedUser.UserId;
                            break;
                    }
                }
            }

            // If UserId is null or auditing skipped, call base without user
            if (_authenticatedUser.UserId == null || SkipAudit)
                return await base.SaveChangesAsync(cancellationToken);

            // Otherwise, pass UserId to base SaveChangesAsync (your current logic)
            return await base.SaveChangesAsync(_authenticatedUser.UserId);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var property in builder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,2)");
            }

            base.OnModelCreating(builder);


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

            var converter = new ValueConverter<List<string>, string>(
            v => JsonConvert.SerializeObject(v),
            v => JsonConvert.DeserializeObject<List<string>>(v));

            #region Converting Dynamic Form Builder Entity Fields in json format

            builder.Ignore<FieldSizeEntity>();
            builder.Entity<FormBuilderEntity>().OwnsOne(
                f => f.FieldsDetails, d =>
                {
                    d.ToJson();
                    d.OwnsMany(d => d.Fields)
                    .OwnsOne(d => d.FieldSize);
                }
                );
            builder.Entity<DraftingDetailEntity>().OwnsMany(
                f => f.FieldDetails, d =>
                {
                    d.ToJson();
                }
                );

            builder.Entity<TemplateInfoEntity>().OwnsMany(
                f => f.Tags, j =>
                {
                    j.ToJson();
                }
                );
            builder.Entity<FormTemplateMappingEntity>().OwnsMany(
                f => f.FieldsMapping, j =>
                {
                    j.ToJson();
                }
                );
            builder.Entity<CaseTitleEntity>().OwnsMany(
                f => f.CaseApplicants, j =>
                {
                    j.ToJson();
                }
                );
            builder.Entity<CaseProcedingEntity>().OwnsOne(
               f => f.ProcWork, j =>
               {
                   j.ToJson();
                   j.OwnsMany(d => d.Works);
               }
               );
            builder.Entity<LanguageEntity>().OwnsMany(
                j => j.Languages, k =>
                {
                    k.ToJson();
                }
                );

            builder.Entity<CourtTypeEntity>().OwnsMany(
               j => j.Languages, k =>
               {
                   k.ToJson();
               }
               );
            builder.Entity<StateEntity>().OwnsMany(
              j => j.Languages, k =>
              {
                  k.ToJson();
              }
              );
            builder.Entity<CaseCategoryEntity>().OwnsMany(
              j => j.Languages, k =>
              {
                  k.ToJson();
              }
              );
            builder.Entity<CaseStageEntity>().OwnsMany(
              j => j.Languages, k =>
              {
                  k.ToJson();
              }
              );
            builder.Entity<MultiLangDictEntity>().OwnsMany(
               j => j.MultiLangs, k =>
               {
                   k.ToJson();
               }
               );
            #endregion

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
    }
}