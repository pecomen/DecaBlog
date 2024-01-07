using DecaBlogMVC.Models;

namespace DecaBlogMVC.Services;

public class ImageService
{
    private readonly HttpClientService _clientService;

    public ImageService(HttpClientService clientService)
    {
        _clientService = clientService;
    }

    public async Task<ImageUploadResponse?> UploadImage(IFormFile image)
    {
        const string apiUrl = "api/images/upload";
        var res = await _clientService.PostFileAsync<ResponseObject<ImageUploadResponse>>(apiUrl, image);

        if (!res.IsSuccessful)
            return null;

        return res.Data;
    }
}

public class ImageUploadResponse
{
    public string? PublicId { get; set; }
    public string? Url { get; set; }
}