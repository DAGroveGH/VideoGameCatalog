using System.Net.Http.Json;
using VideoGameCatalog.Shared.Models;

namespace VideoGameCatalog.Client.Services
{
    public class VideoGameCSVApiService
    {
        private readonly HttpClient _httpClient;

        public VideoGameCSVApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            Console.WriteLine("VideoGameCSVApiService initialized with HttpClient base address: " + _httpClient.BaseAddress);
        }

        public async Task<IReadOnlyList<VideoGameModel>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<VideoGameModel>>("api/games")
                ?? new List<VideoGameModel>();
        }

        public async Task<VideoGameModel?> GetVideoGameCSVByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<VideoGameModel>($"api/videogamescsv/{id}");
        }
    }
}
