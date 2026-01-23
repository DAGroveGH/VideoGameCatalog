using System.Globalization;
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

        public async Task<IReadOnlyList<VideoGameModel>> GetAllVideoGamesCSVAsync()
        {
            try
            {
                var games = new List<VideoGameModel>();
                games = await _httpClient.GetFromJsonAsync<List<VideoGameModel>>("api/videogamescsv");

                return games ?? [];
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error fetching video games: " + ex.Message);

                return [];
            }
        }

        public async Task<VideoGameModel?> GetVideoGameCSVByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<VideoGameModel>($"api/videogamescsv/{id}");
        }
    }
}
