using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using System;

namespace CourtApp.Web.Extensions
{
    public static class UpDownFileExtentions
    {
        public static async Task<string> UploadImage(this IFormFile Image, string env, string folderName)
        {
            string path = env + folderName;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fileName = Image.FileName;
            fileName = Guid.NewGuid().ToString() + fileName;
            path = env + folderName + fileName;
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await Image.CopyToAsync(stream);
            }
            return fileName;
        }        
    }
}
