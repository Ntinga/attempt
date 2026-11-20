public class FileService : IFileService
{
    private readonly IHttpClientFactory
        _httpClientFactory;

    public FileService(
        IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory =
            httpClientFactory;
    }

    public async Task<string?>
        UploadFileAsync(IFormFile file)
    {
        var client =
            _httpClientFactory
                .CreateClient("RaceDayApi");

        using var content =
            new MultipartFormDataContent();

        using var stream =
            file.OpenReadStream();

        content.Add(
            new StreamContent(stream),
            "file",
            file.FileName);

        var response =
            await client.PostAsync(
                "api/files/upload",
                content);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var result =
            await response.Content.ReadAsStringAsync();

        return result;
    }
}