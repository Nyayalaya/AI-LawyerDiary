using System.Collections.Generic;

namespace CourtApp.Web.Models
{
    public class UploadSettings
    {
        public string Provider { get; set; }
        public Dictionary<string, string> Folders { get; set; }
        public AzureBlobConfig AzureBlob { get; set; }
        public GoogleDriveConfig GoogleDrive { get; set; }
        public LocalConfig Local { get; set; }
    }

    public class AzureBlobConfig
    {
        public string ConnectionString { get; set; }
        public string ContainerName { get; set; }
        public string BaseUrl { get; set; }
    }

    public class GoogleDriveConfig
    {
        //public string ClientId { get; set; }
        //public string ClientSecret { get; set; }
        //public string RefreshToken { get; set; }
        public string BaseFolderId { get; set; }
        public string ServiceAccountKeyFilePath { get; set; }
        public string ApplicationName { get; set; }
        public string DownloadBaseUri { get; set; }
    }

    public class LocalConfig
    {
        public string RootPath { get; set; }
    }

}
