using CourtApp.Application.Interfaces.Shared;
using System.IO;
using System.Threading.Tasks;

namespace CourtApp.Web.Services
{
    public class LocalUploaderService : IDocumentUploadService
    {
        public Task<bool> DeleteFileAsync(string fileId)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> UploadCompressedFileAsync(Stream zipStream, string zipFileName, string documentType)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> UploadFileAsync(Stream zipStream, string zipFileName, string documentType)
        {
            throw new System.NotImplementedException();
        }
    }
}
