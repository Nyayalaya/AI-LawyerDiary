using Microsoft.SemanticKernel;

namespace CourtApp.Infrastructure.AI.Kernels.Manager;

public interface IKernelManager
{
    Kernel GetKernel();

    Kernel GetKernel(string provider);

    void Refresh();

    void Refresh(string provider);
}