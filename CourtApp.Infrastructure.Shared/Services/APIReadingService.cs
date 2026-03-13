using CourtApp.Application.Interfaces.Shared;
using System;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Shared.Services
{
    internal class APIReadingService : IExternalAPIReadingService<object>
    {
        //public async Task<T> GetAPIData(string url, string method, string token)
        //{
        //    using var httpClient = new HttpClient();
        //    httpClient.BaseAddress = new Uri(url);
        //    httpClient.DefaultRequestHeaders.Add("Authorization", token);
        //    var response = httpClient.GetAsync(url).Result;
        //    if (response.IsSuccessStatusCode)
        //    {
        //        return await response.Content.ReadFromJsonAsync<T>();
        //    }
        //    return await response.Content.ReadFromJsonAsync<T>();
        //}

        Task<object> IExternalAPIReadingService<object>.GetAPIData(string url, string method, string authheader)
        {
            throw new NotImplementedException();
        }
    }
}
