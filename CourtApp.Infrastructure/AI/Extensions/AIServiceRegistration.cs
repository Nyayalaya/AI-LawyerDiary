using CourtApp.Infrastructure.AI.Kernels.Builders;
using CourtApp.Infrastructure.AI.Kernels.Factory;
using CourtApp.Infrastructure.AI.Kernels.Manager;
using CourtApp.Infrastructure.AI.Kernels.Registration;
using CourtApp.Infrastructure.AI.Plugins.CasePlugin;
using CourtApp.Infrastructure.AI.Plugins.DocumentPlugin;
using CourtApp.Infrastructure.AI.Prompts;
using CourtApp.Infrastructure.AI.Providers.Factory;
using CourtApp.Infrastructure.AI.Configuration.Core;
using CourtApp.Infrastructure.AI.Providers.Gemini;
using CourtApp.Infrastructure.AI.Services;
using LawyerDiary.Infrastructure.AI.Kernels.Factory;
using LawyerDiary.Infrastructure.AI.Kernels.Registration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CourtApp.Infrastructure.AI.Providers.Interfaces;
namespace CourtApp.Infrastructure.AI.Extensions
{
    public static class AIServiceRegistration
    {
        public static IServiceCollection AddAIServices(this IServiceCollection services, IConfiguration configuration) 
        {
            // Bind options from configuration
            services.Configure<AIOptions>(configuration
                .GetSection(AIOptions.SectionName));

            services.AddScoped<IKernelBuilder, GeminiKernelBuilder>();
            services.AddScoped<IKernelFactory, KernelFactory>();
            services.AddScoped<IPluginRegistration, PluginRegistration>();
            services.AddScoped<IKernelManager, KernelManager>();
            services.AddSingleton<ISystemPromptProvider, SystemPromptProvider>();
            services.AddScoped<IPromptBuilder, PromptBuilder>();
            services.AddScoped<IAIProviderFactory, AIProviderFactory>();
            // Register default provider implementations (simple in-project Gemini provider)
            services.AddScoped<GeminiChatService>();
            services.AddScoped<GeminiEmbeddingService>();
            services.AddScoped<IChatProvider, GeminiProvider>();

            // Register the high-level assistant
            services.AddScoped<ILawyerAssistant, LawyerAssistant>();

            services.AddScoped<DocumentPlugin>();
            services.AddScoped<CasePlugin>();
            return services;
        }
    }
}
