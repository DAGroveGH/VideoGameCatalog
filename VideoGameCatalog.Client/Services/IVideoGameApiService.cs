using VideoGameCatalog.Shared.Models;

namespace VideoGameCatalog.Client.Services
{
    public interface IVideoGameApiService
    {
        Task<IReadOnlyList<VideoGameModel>> GetAllVideoGamesAsync();
        Task<VideoGameModel?> GetVideoGameByIdAsync(int id);
    }
}
