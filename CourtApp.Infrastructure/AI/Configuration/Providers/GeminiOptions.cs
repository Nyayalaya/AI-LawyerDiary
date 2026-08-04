
namespace CourtApp.Infrastructure.AI.Configuration.Providers
{
    public sealed class GeminiOptions
    {
        public string ApiKey { get; set; } = string.Empty;
        public string Endpoint { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string EmbeddingModel { get; set; } = string.Empty;
        public string VisionModel { get; set; } = string.Empty;
        public double Temperature { get; set; }
        public double TopP { get; set; }
        public int TopK { get; set; }
        public int CandidateCount { get; set; }
        public int MaxOutputTokens { get; set; }
        public bool EnableStreaming { get; set; }
        public bool EnableThinking { get; set; }
        public bool EnableGrounding { get; set; }
    }
}
