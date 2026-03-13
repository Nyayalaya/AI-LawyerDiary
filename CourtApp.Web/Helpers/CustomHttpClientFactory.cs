
using Google.Apis.Http;
using System;
using System.Net.Http;

public class CustomHttpClientFactory : Google.Apis.Http.IHttpClientFactory
{
    public ConfigurableHttpClient CreateHttpClient(CreateHttpClientArgs args)
    {
        var httpClientHandler = new HttpClientHandler();

        var messageHandler = new ConfigurableMessageHandler(httpClientHandler);

        var configurableClient = new ConfigurableHttpClient(messageHandler)
        {
            Timeout = TimeSpan.FromMinutes(5) // ✅ Set timeout here
        };

        return configurableClient;
    }
}
