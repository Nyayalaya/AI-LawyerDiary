
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CourtApp.Infrastructure.Extensions
{
    public static class InfrastructureServiceExtensions
    {

        #region 🔹 ENTRY POINT

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            return services
               .AddDatabase(configuration)              // DB
               .AddIdentityLayer()                     // Identity
               .AddDomainServices()                    // Business Services
               .AddCommonServices()                    // Shared Services
               .AddPersistenceContexts(configuration)  // DbContext Abstraction
               .AddRepositories()                      // Repositories
               .AddCacheRepositories();                // Cache Layer
        }

        #endregion
        
    }
}
