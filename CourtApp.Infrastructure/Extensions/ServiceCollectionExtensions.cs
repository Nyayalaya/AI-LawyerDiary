using CourtApp.Application.Features.Auth.Services;
using CourtApp.Application.Features.Cadre.Services;
using CourtApp.Application.Features.CaseCategory.Services;
using CourtApp.Application.Features.CaseDocuments.Services;
using CourtApp.Application.Features.CaseStage.Services;
using CourtApp.Application.Features.CaseType.Services;
using CourtApp.Application.Features.CourtHall.Interfaces;
using CourtApp.Application.Features.CourtLevel.Services;
using CourtApp.Application.Features.CourtType.Services;
using CourtApp.Application.Features.FormManagement.Interfaces;
using CourtApp.Application.Features.Permission.Services;
using CourtApp.Application.Features.Profile.Services;
using CourtApp.Application.Features.State.Services;
using CourtApp.Application.Features.SystemUsers.Services;

using CourtApp.Application.Interfaces.CacheRepositories;
using CourtApp.Application.Interfaces.CacheRepositories.Common;
using CourtApp.Application.Interfaces.CacheRepositories.FormBuilder;

using CourtApp.Application.Interfaces.Contexts;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Application.Interfaces.Repositories.Accounting;
using CourtApp.Application.Interfaces.Repositories.Common;
using CourtApp.Application.Interfaces.Repositories.FormBuilder;

using CourtApp.Application.Interfaces.Shared;

using CourtApp.Infrastructure.CacheRepositories;
using CourtApp.Infrastructure.DbContexts;
using CourtApp.Infrastructure.Identity.Models;
using CourtApp.Infrastructure.Identity.Services;
using CourtApp.Infrastructure.Repositories;
using CourtApp.Infrastructure.Services;
using CourtApp.Infrastructure.Shared.Services;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

namespace CourtApp.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        #region 🔹 DATABASE

        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var useInMemory = configuration.GetValue<bool>("UseInMemoryDatabase");

            if (useInMemory)
            {
                services.AddDbContext<IdentityContext>(o => o.UseInMemoryDatabase("IdentityDb"));
                services.AddDbContext<ApplicationDbContext>(o => o.UseInMemoryDatabase("ApplicationDb"));
            }
            else
            {
                var connection = configuration.GetConnectionString("Postgres");

                services.AddDbContext<IdentityContext>(o => o.UseNpgsql(connection));

                services.AddDbContext<ApplicationDbContext>(o =>
                    o.UseNpgsql(connection, x => x.UseVector())); // ✅ pgvector
            }

            return services;
        }

        #endregion

        #region 🔹 IDENTITY

        public static IServiceCollection AddIdentityLayer(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<IdentityContext>()
            .AddDefaultTokenProviders();

            return services;
        }

        #endregion

        #region 🔹 DOMAIN SERVICES

        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            #region 🔐 AUTH

            services.AddTransient<IIdentityService, IdentityService>();
            services.AddScoped<IRegistrationService, RegistrationService>();
            services.AddScoped<IChangePasswordService, ChangePasswordService>();

            #endregion

            #region 🔐 PERMISSION

            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IUserPermissionService, UserPermissionService>();
            services.AddScoped<IRolePermissionService, RolePermissionService>();

            #endregion

            #region 👤 USER

            services.AddScoped<IUserHierarchyService, UserHierarchyService>();
            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddScoped<IUserBillingInfoService, UserBillingInfoService>();
            services.AddScoped<IUserBasicInfoService, UserBasicInfoService>();
            services.AddScoped<IUserOrganizationService, UserOrganizationService>();
            services.AddScoped<IUserApprovalService, UserApprovalService>();
            services.AddScoped<ISystemUserService, SystemUserService>();

            #endregion

            return services;
        }

        #endregion

        #region 🔹 COMMON SERVICES

        public static IServiceCollection AddCommonServices(this IServiceCollection services)
        {
            services.AddScoped<ICurrentRequestProvider, CurrentRequestProvider>();
            services.AddScoped<IDateTimeService, SystemDateTimeService>();
            services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IMailService, SMTPMailService>();

            services.AddDistributedMemoryCache();

            return services;
        }

        #endregion

        #region 🔹 DB CONTEXT ABSTRACTION

        public static IServiceCollection AddPersistenceContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(cfg => { }, Assembly.GetExecutingAssembly());

            services.AddScoped<IApplicationDbContext>(provider =>
                provider.GetRequiredService<ApplicationDbContext>());

            return services;
        }

        #endregion

       

        #region 🔹  REPOSITORIES

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ILogRepository, LogRepository>();

            // 👉 KEEP ALL YOUR EXISTING REGISTRATIONS (NO CHANGE)
            services.AddScoped<IBookTypeRepository, BookTypeRepository>();
            services.AddScoped<IBookMasterRepository, BookMasterRepository>();
            services.AddScoped<IPublicationRepository, PublisheRepository>();

            services.AddScoped<ICaseKindRepository, CaseKindRepository>();
            services.AddScoped<ICaseCategoryRepository, CaseNatureRepository>();
            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.AddScoped<ICaseStageRepository, CaseStageRepository>();
            services.AddScoped<ICaseTypeRepository, TypeOfCasesRepository>();

            services.AddScoped<ICourtRepository, CourtRepository>();
            services.AddScoped<ICourtMasterRepository, CourtMasterRepository>();
            services.AddScoped<ICourtLevelMasterRepository, CourtLevelMasterRepository>();
            services.AddScoped<ICourtTypeRepository, CourtTypeRepository>();
            services.AddScoped<ICourtDistrictRepository, CourtDistrictRepository>();
            services.AddScoped<ICourtComplexRepository, CourtComplexRepository>();
            services.AddScoped<ICourtBenchRepository, CourtBenchRepository>();
            services.AddScoped<ICourtHallRepository, CourtHallRepository>();
            services.AddScoped<IFormTypeRepository, FormTypeRepository>();

            services.AddScoped<ILocationRepository, LocationRepository>();

            services.AddScoped<IStateMasterRepository, StateMasterRepository>();
            services.AddScoped<IDistrictMasterRepository, DistrictMasterRepository>();

            services.AddScoped<ICaseManagmentRepository, CaseManagmentRepository>();
            services.AddScoped<ICaseTitleRepository, CaseTitleRepository>();
            services.AddScoped<ICaseDocsRepository, CaseDocsRepository>();
            services.AddScoped<ICaseProceedingRepository, CaseProceedingRepository>();
            services.AddScoped<ICaseWorkRepository, CaseWorkRepository>();
            services.AddScoped<ICaseAgainstRepository, CaseAgainstRepository>();
            services.AddScoped<ICaseAssignedRepository, CaseAssignedRepository>();
            services.AddScoped<ICaseHelperRepository, CaseHelperRepository>();

            services.AddScoped<ICaseDraftingRepository, CaseDraftingRepository>();

            services.AddScoped<IProceedingHeadRepository, ProceedingHeadRepository>();
            services.AddScoped<IProceedingSubHeadRepository, ProceedingSubHeadRepository>();
            services.AddScoped<IWorkMasterRepository, WorkMasterRepository>();
            services.AddScoped<IWorkMasterSubRepository, WorkMasterSubRepository>();

            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IUserCaseRepository, UserCaseRepository>();

            services.AddScoped<ICourtFeeStructureRepository, CourtFeeStructureRepository>();
            services.AddScoped<IBillingDetailRepository, BillingDetailRepository>();

            services.AddScoped<ILawyerRepository, LawyerMasterRepository>();
            services.AddScoped<ISpecilityRepository, SpecilityRepository>();

            services.AddScoped<IFormBuilderRepository, FormBuilderRepository>();
            services.AddScoped<ITemplateInfoRepository, TemplateInfoRepository>();
            services.AddScoped<IFormTempMappingRepository, FormTempMappingRepository>();

            services.AddScoped<ILanguageRepository, LanguageRepository>();
            services.AddScoped<IMultiLangWordRepository, MultiLangWordRepository>();
            services.AddScoped<ICourtFormTypeRepository, CourtFormTypeRepository>();

            services.AddScoped<IFSTitleRepository, FSTitleRepository>();
            services.AddScoped<IDOTypeRepository, DOTypeRepository>();

            services.AddScoped<ICadreMasterRepository, CadreMasterRepository>();

            // 🔹 FORM MANAGEMENT REPOSITORIES
            services.AddScoped<IFormTypeRepository, FormTypeRepository>();
            services.AddScoped<IFormMasterRepository, FormMasterRepository>();
            services.AddScoped<IFormSubtypeRepository, FormSubtypeRepository>();
            services.AddScoped<IFormTemplateRepository, FormTemplateRepository>();
            services.AddScoped<IFormTemplateVersionRepository, FormTemplateVersionRepository>();
            services.AddScoped<IFormCaseCategoryMappingRepository, FormCaseCategoryMappingRepository>();
            services.AddScoped<IFormCourtMappingRepository, FormCourtMappingRepository>();

            return services;
        }

        #endregion

        #region 🔹 CACHE

        public static IServiceCollection AddCacheRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICourtFeeStructureCacheRepository, CourtFeeStructureCacheRepository>();
            services.AddScoped<IBookTypeCacheRepository, BookTypeCacheRepository>();
            services.AddScoped<IBookMasterCacheRepository, BookMasterCacheRepository>();
            services.AddScoped<IPublicationCacheRepository, PublisherCacheRepository>();

            services.AddScoped<ICaseKindCacheRepository, CaseKindCacheRepository>();
            services.AddScoped<ICaseCategoryCacheRepository, CaseNatureCacheRepository>();
            services.AddScoped<ISubjectCacheRepository, SubjectCacheRepository>();
            services.AddScoped<ICaseStageCacheRepository, CaseStageCacheRepository>();
            services.AddScoped<ICaseTypeCacheRepository, TypeOfCasesCacheRepository>();

            services.AddScoped<ICourtCacheRepository, CourtCacheRepository>();
            services.AddScoped<ICourtMasterCacheRepository, CourtMasterCacheRepository>();
            services.AddScoped<ICourtLevelCacheRepository, CourtLevelCacheRepository>();
            services.AddScoped<ICourtTypeCacheRepository, CourtTypeCacheRepository>();

            services.AddScoped<IStateCacheRepository, StateMasterCacheRepository>();
            services.AddScoped<IDsitrictMasterCacheRepository, DistrictMasterCacheRepository>();

            services.AddScoped<ICourtDistrictCacheRepository, CourtDistrictCacheRepository>();
            services.AddScoped<ICourtComplexCacheRepository, CourtComplexCacheRepository>();
            services.AddScoped<ICourtHallCacheRepository, CourtHallCacheRepository>();

            services.AddScoped<IClientCacheRepository, ClientCacheRepository>();
            services.AddScoped<IUserCaseCacheRepository, UserCaseCacheRepository>();

            services.AddScoped<ICaseDraftingCacheRepository, CaseDraftingCacheRepository>();

            services.AddScoped<ICadreMasterCacheRepository, CadreMasterCacheRepository>();
            services.AddScoped<IFSTitleCacheRepository, FSTitleCacheRepository>();
            services.AddScoped<ILawyerCacheRepository, LawyerCacheRepository>();

            services.AddScoped<IFormBuilderCacheRepository, FormBuilderCacheRepository>();
            services.AddScoped<ITemplateInfoCacheRepository, TemplateInfoCacheRepository>();

            services.AddScoped<ISpecilityCacheRepository, SpecilityCacheRepository>();

            services.AddScoped<IMultiLangWordCacheRepository, MultiLangDictCacheRepository>();

            services.AddScoped<IDOTypeCacheRepository, DOTypeCacheRepository>();
            services.AddScoped<ILocationCacheRepository, LocationCacheRepository>();

            return services;
        }

        #endregion
    }
}
