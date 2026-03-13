using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Web.Services
{
    public class BlobService
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;
        public BlobService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration["AzureBlobStorage:ConnectionString"];
        }

        public string BaseUri()
        {
            string basePath = _configuration["AzureBlobStorage:BaseUrl"];
            return basePath;
        }
        private BlobContainerClient GetContainerClient(string containerName)
        {
            var containerClient = new BlobContainerClient(_connectionString, containerName);
            containerClient.CreateIfNotExists(PublicAccessType.Blob);
            return containerClient;
        }
        public async Task<string> UploadOrUpdateFileAsync(Stream fileStream, string fileName, string contentType, string documentType, CancellationToken cancellationToken)
        {
            try
            {
                // Determine the correct container based on the document type
                string containerName = documentType switch
                {
                    "ProfileImage" => _configuration["AzureBlobStorage:Containers:ProfileImages"],
                    "Draft" => _configuration["AzureBlobStorage:Containers:DraftDocuments"],
                    "Order" => _configuration["AzureBlobStorage:Containers:OrderDocuments"],
                    _ => throw new ArgumentException("Invalid document type")
                };

                // Get the container client
                var containerClient = GetContainerClient(containerName);

                // Create a BlobClient for the target blob in Azure Blob Storage
                BlobClient blobClient = containerClient.GetBlobClient(fileName);

                // Set up the HTTP headers for the blob
                var blobHttpHeaders = new BlobHttpHeaders { ContentType = contentType };

                // Upload the file in chunks (using StreamUploadOptions)
                var uploadOptions = new BlobUploadOptions
                {
                    HttpHeaders = blobHttpHeaders,
                    TransferOptions = new StorageTransferOptions
                    {
                        MaximumConcurrency = 4,  // Number of concurrent upload threads (adjust as necessary)
                        MaximumTransferSize = 4 * 1024 * 1024  // Set to 4MB per chunk (adjust as needed)
                    }
                };

                // Upload the file to the Blob Storage with cancellation support
                await blobClient.UploadAsync(fileStream, uploadOptions, cancellationToken);
                return $"{containerName}/{fileName}";
                // Return the URI of the uploaded file
                //return blobClient.Uri.ToString();
            }
            catch (OperationCanceledException)
            {
                // Handle operation cancellation (optional)
                throw new Exception("File upload was canceled.");
            }
            catch (Exception ex)
            {
                // Log or handle the exception (more specific error handling can be added as needed)
                throw new Exception($"File upload failed: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteFileAsync(string fileUrl)
        {
            try
            {
                if (string.IsNullOrEmpty(fileUrl)) return false;

                Uri uri = new Uri(fileUrl);
                string blobName = uri.AbsolutePath.TrimStart('/').Split('/', 2)[1]; // Extract blob name

                string containerName = uri.AbsolutePath.TrimStart('/').Split('/')[0]; // Extract container name
                var containerClient = GetContainerClient(containerName);
                BlobClient blobClient = containerClient.GetBlobClient(blobName);

                return await blobClient.DeleteIfExistsAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"File deletion failed: {ex.Message}");
            }
        }
    }
}
