namespace LawyerDiary.Infrastructure.AI.Kernels.Registration;

using Microsoft.SemanticKernel;

public interface IPluginRegistration
{
    void RegisterPlugins(Kernel kernel);
}