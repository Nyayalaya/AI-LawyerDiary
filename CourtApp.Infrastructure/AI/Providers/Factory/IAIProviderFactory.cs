using CourtApp.Infrastructure.AI.Providers.Interfaces;

namespace CourtApp.Infrastructure.AI.Providers.Factory;

public interface IAIProviderFactory
{
    IChatProvider GetCurrentProvider();

    IChatProvider GetProvider(string providerName);
}