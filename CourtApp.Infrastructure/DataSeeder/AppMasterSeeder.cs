using CourtApp.Infrastructure.DbContexts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.DataSeeder
{
    public static class AppMasterSeeder
    {
        public static async Task SeedAsync(IServiceProvider service)
        {
            using var scope = service.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>(); 
            var identityContext = scope.ServiceProvider.GetRequiredService<IdentityContext>();
            await StateSeeder.SeedStatesAsync(context, identityContext);
            await CourtLevelSeeder.SeedCourtLevelAsync(context,identityContext);
            await CourtTypeSeeder.SeedCourtTypeAsync(context, identityContext);
            await CaseStageSeeder.SeedCaseStagesAsync(context, identityContext);
            await CaseCategorySeeder.SeedCaseCategoriesAsync(context, identityContext);
            await CadreSeeder.SeedAsync(context, identityContext);
            await CourtFormSeeder.SeedAsync(context, identityContext);
            await FormCaseCategoryMappingSeeder.SeedAsync(context, identityContext);
        }
    }
}
