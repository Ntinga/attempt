namespace RaceDay.API.Services
{
    public interface IBlobStorageService
    {
        Task<string> UploadFileAsync(IFormFile file, string containerName);
    }
}