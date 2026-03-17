using CourtApp.Application.Features.Auth.Services;
using CourtApp.Application.Interfaces.Shared;
using CourtApp.Infrastructure.DbContexts;
using CourtApp.Infrastructure.Identity.Models;
using CourtApp.Infrastructure.Identity.Services;
using CourtApp.Infrastructure.Services;
using CourtApp.Infrastructure.Shared.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CourtApp.Infrastructure.Extensions
{
    public static class InfrastructureServiceExtensions
    {
        public static void AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<IdentityContext>(
                    options => options.UseInMemoryDatabase("IdentityDb"),
                    ServiceLifetime.Transient);

                services.AddDbContext<ApplicationDbContext>(
                    options => options.UseInMemoryDatabase("ApplicationDb"));
            }
            else
            {
                services.AddDbContext<IdentityContext>(
                    options => options.UseNpgsql(configuration.GetConnectionString("Postgres")),
                    ServiceLifetime.Transient);

                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseNpgsql(
                        configuration.GetConnectionString("Postgres"),
                        o => o.UseVector()   // ✅ IMPORTANT
                    ));
            }

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
                options.Password.RequireNonAlphanumeric = false;
            })
           .AddEntityFrameworkStores<IdentityContext>()
           .AddDefaultTokenProviders();

            services.AddTransient<IIdentityService, IdentityService>();
        }

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPersistenceContexts(configuration);
            services.AddRepositories();
            services.AddScoped<ICurrentRequestProvider, CurrentRequestProvider>();
            services.AddScoped<IDateTimeService, SystemDateTimeService>();
            services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();
            services.AddScoped<IMailService, SMTPMailService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddDistributedMemoryCache();
            return services;
        }
    }
}
