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
        DbSet<StateEntity> States { get; set; }
        DbSet<DistrictEntity> Districts { get; set; }
        DbSet<BookTypeEntity> BookTypes { get; set; }
        DbSet<LDBookEntity> LDBooks { get; set; }
        DbSet<PublisherEntity> Publishers { get; set; }
        DbSet<ClientEntity> Clients { get; set; }
       
        DbSet<TypeOfCasesEntity> Typeofcases { get; set; }
        DbSet<CaseKindEntity> CaseKinds { get; set; }
        DbSet<CaseDetailEntity> Cases { get; set; }
        DbSet<LawyerMasterEntity> Laywers { get; set; }
        DbSet<ProceedingHeadEntity> ProceedingHeads { get; set; }
        DbSet<ProceedingSubHeadEntity> ProceedingSubHeads { get; set; }
        DbSet<WorkTypeEntity> WorkMasters { get; set; }
        DbSet<WorksEntity> WorkMasterSubs { get; set; }
        DbSet<CaseDetailAgainstEntity> AgainstCaseDetails { get; set; }
        DbSet<CaseTitleEntity> CaseTitiles { get; set; }
        DbSet<CourtBenchEntity> CourtBenchEntities { get; set; }
        DbSet<CaseProcedingEntity> CaseProcedingEntities { get; set; }
        DbSet<CaseWorkEntity> CaseWorkEntities { get; set; }
        DbSet<DOTypeEntity> DOTypeEntities { get; set; }
        DbSet<CaseDocsEntity> caseDocsEntities { get; set; }
        DbSet<FSTitleEntity> FSTitleEntities { get; set; }
        DbSet<FormBuilderEntity> DynamicFrmBuilders { get; set; }
        DbSet<DraftingDetailEntity> CaseTempMappings { get; set; }
        DbSet<FormTemplateMappingEntity> TempFormMappings { get; set; }
        DbSet<CadreMasterEntity> Cadres { get; set; }
        DbSet<Specialization> Specilities { get; set; }
        DbSet<AssignCaseEntity> AssignedCases { get; set; }
        DbSet<LanguageEntity> LanguageEntities { get; set; }
        DbSet<CourtFormTypeEntity> CourtFormTypeEntities { get; set; }
        DbSet<BillingDetailEntity> BillingDetails { get; set; }
        public DbSet<AIConversation> AIConversations { get; set; }
        public DbSet<DocumentChunk> DocumentChunks { get; set; }
        public DbSet<LegalCitationEntity> LegalCitations { get; set; }
        public DbSet<DocumentChunkEmbedding> ChunkEmbeddings { get; set; }

        public DbSet<LocationEntity> Locations { get; set; }
        public DbSet<CourtEntity> Courts { get; set; }
        DbSet<CourtTypeEntity> CourtTypes { get; set; }
        public DbSet<CourtLevelEntity> CourtLevels { get; set; }
        public DbSet<JudgeEntity> Judges { get; set; }
        public DbSet<CourtComplexEntity> CourtComplexes { get; set; }
        public DbSet<CourtHallEntity> CourtHalls { get; set; }


    }
}