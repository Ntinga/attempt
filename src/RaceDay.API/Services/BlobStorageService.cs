using Azure.Storage.Blobs;

namespace RaceDay.API.Services
{
    public class BlobStorageService
        : IBlobStorageService
    {
        private readonly IConfiguration _configuration;

        public BlobStorageService(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> UploadFileAsync(
            IFormFile file,
            string containerName)
        {
            var connectionString =
                _configuration.GetConnectionString(
                    "AzureBlobStorage");

            var containerClient =
                new BlobContainerClient(
                    connectionString,
                    containerName);

            await containerClient
                .CreateIfNotExistsAsync();

            var blobClient =
                containerClient.GetBlobClient(
                    Guid.NewGuid()
                    + Path.GetExtension(
                        file.FileName));

            using var stream =
                file.OpenReadStream();

            await blobClient.UploadAsync(
                stream,
                true);

            return blobClient.Uri.ToString();
        }
    }
}