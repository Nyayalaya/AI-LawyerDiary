using System.IO;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Shared
{
    public interface IDocumentUploadService
    {
        Task<string> UploadFileAsync(Stream zipStream, string zipFileName, string documentType);

        Task<bool> DeleteFileAsync(string fileId);
    }
}
