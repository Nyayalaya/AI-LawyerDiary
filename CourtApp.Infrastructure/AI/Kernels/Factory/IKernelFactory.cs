using Microsoft.SemanticKernel;

namespace LawyerDiary.Infrastructure.AI.Kernels.Factory;

public interface IKernelFactory
{
    Kernel CreateKernel();

    Kernel CreateKernel(string provider);
}