using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Shared.Services
{
    public class WhatsAppService
    {
        private readonly string _apiUrl;
        private readonly string _apiKey;
        public ILogger<WhatsAppService> _logger { get; }
        public WhatsAppService(IConfiguration configuration, ILogger<WhatsAppService> _logger)
        {
            _apiUrl = configuration["WhatsAppSettings:ApiUrl"];
            _apiKey = configuration["WhatsAppSettings:ApiKey"];
            this._logger = _logger;
        }
        public async Task SendWhatsAppMessageAsync(string toPhoneNumber, string message)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
                    var payload = new
                    {
                        to = toPhoneNumber,
                        type = "text",
                        text = new { body = message }
                    };
                    var jsonPayload = JsonConvert.SerializeObject(payload);
                    var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync($"{_apiUrl}/messages", content);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Message sent successfully");
                    }
                    else
                    {
                        Console.WriteLine($"Error sending message: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
        }
    }
}
