using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Shared
{
    public interface IExternalAPIReadingService<T> where T : class
    {
        Task<T> GetAPIData(string url, string method, string authheader);
    }
}
