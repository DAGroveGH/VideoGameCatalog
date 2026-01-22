using VideoGameCatalog.Shared.Models;

namespace VideoGameCatalog.Server.Repositories
{
    public interface IVideoGameRepository
    {
        IReadOnlyList<VideoGameModel> GetAllVideoGames();
        VideoGameModel? GetVideoGameById(int id);
    }
}
