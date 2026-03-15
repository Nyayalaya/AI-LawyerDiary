using CourtApp.Worker.Jobs;
using Microsoft.Extensions.DependencyInjection;

namespace CourtApp.Worker
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddJobs(this IServiceCollection services)
        {
            services.AddScoped<DocumentPipelineJob>();
            services.AddScoped<TextExtractionJob>();
            services.AddScoped<ChunkGenerationJob>();
            services.AddScoped<EmbeddingGenerationJob>();
            services.AddScoped<CitationExtractionJob>();

            return services;
        }
    }
}
