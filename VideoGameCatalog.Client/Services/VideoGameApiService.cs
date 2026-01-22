using System.Net.Http.Json;
using VideoGameCatalog.Shared.Models;

namespace VideoGameCatalog.Client.Services
{
    public class VideoGameApiService : IVideoGameApiService
    {
        private readonly HttpClient _httpClient;

        public VideoGameApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            Console.WriteLine("VideoGameApiService initialized with HttpClient base address: " + _httpClient.BaseAddress);
        }
        public async Task<IReadOnlyList<VideoGameModel>> GetAllVideoGamesAsync()
        {
            try
            {
                var games = new List<VideoGameModel>();
                games = await _httpClient.GetFromJsonAsync<List<VideoGameModel>>("api/videogames");

                return games ?? [];
            } catch (Exception ex) { 

                Console.WriteLine("Error fetching video games: " + ex.Message);

                return [];
            }
        }

        public async Task<VideoGameModel?> GetVideoGameByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<VideoGameModel>($"api/videogames/{id}");
        }
    }
}
