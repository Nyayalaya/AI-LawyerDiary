using Microsoft.SemanticKernel;
namespace CourtApp.Infrastructure.AI.Kernels.Builders
{
    public interface IKernelBuilder
    {
        /// <summary>
        /// Provider Name
        /// Example: Gemini, OpenAI
        /// </summary>
        string ProviderName { get; }

        /// <summary>
        /// Creates configured Semantic Kernel
        /// </summary>
        Kernel Build();
    }
}
