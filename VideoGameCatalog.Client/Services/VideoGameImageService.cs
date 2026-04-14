using System.Net.Http.Json;

namespace VideoGameCatalog.Client.Services;

public class VideoGameImageService
{
    private readonly HttpClient _http;

    public VideoGameImageService(HttpClient http)
    {
        _http = http;
    }

    public async Task<Dictionary<string, string?>> GetImagesAsync(IEnumerable<string> titles)
    {
        var response = await _http.PostAsJsonAsync("api/gameimages", titles);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<Dictionary<string, string?>>()
               ?? new();
    }
}