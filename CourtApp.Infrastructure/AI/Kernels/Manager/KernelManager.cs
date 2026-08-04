using CourtApp.Infrastructure.AI.Configuration.Core;
using CourtApp.Infrastructure.AI.Kernels.Manager;
using LawyerDiary.Infrastructure.AI.Kernels.Factory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;

public sealed class KernelManager : IKernelManager
{
    private readonly IKernelFactory _kernelFactory;
    private readonly AIOptions _options;
    private readonly ILogger<KernelManager> _logger;

    public KernelManager(
        IKernelFactory kernelFactory,
        IOptions<AIOptions> options,
        ILogger<KernelManager> logger)
    {
        _kernelFactory = kernelFactory;
        _options = options.Value;
        _logger = logger;
    }

    public Kernel GetKernel()
    {
        return GetKernel(_options.Provider.CurrentProvider);
    }

    public Kernel GetKernel(string provider)
    {
        _logger.LogInformation(
            "Creating kernel for provider {Provider}",
            provider);

        return _kernelFactory.CreateKernel(provider);
    }

    public void Refresh()
    {
        throw new System.NotImplementedException();
    }

    public void Refresh(string provider)
    {
        throw new System.NotImplementedException();
    }
}