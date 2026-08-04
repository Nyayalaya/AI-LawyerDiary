using CourtApp.Infrastructure.AI.Constants;
using CourtApp.Infrastructure.AI.Plugins.CasePlugin;
using CourtApp.Infrastructure.AI.Plugins.DocumentPlugin;
using LawyerDiary.Infrastructure.AI.Kernels.Registration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using System;


namespace CourtApp.Infrastructure.AI.Kernels.Registration;

public sealed class PluginRegistration : IPluginRegistration
{
    private readonly IServiceProvider _serviceProvider;

    public PluginRegistration(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    public void RegisterPlugins(Kernel kernel)
    {
        RegisterDocumentPlugin(kernel);
        RegisterCasePlugin(kernel);
    }

    private void RegisterDocumentPlugin(Kernel kernel)
    {
        var plugin =
            _serviceProvider.GetRequiredService<DocumentPlugin>();

        kernel.Plugins.AddFromObject(
            plugin,
            PluginConstants.DocumentPlugin);
    }

    private void RegisterCasePlugin(Kernel kernel)
    {
        var plugin =
            _serviceProvider.GetRequiredService<CasePlugin>();

        kernel.Plugins.AddFromObject(
            plugin,
            PluginConstants.CasePlugin);
    }
}   
