using VideoGameCatalog.Server.Data;
using VideoGameCatalog.Shared.Models;

namespace VideoGameCatalog.Server.Repositories
{
    public class VideoGameRepository : IVideoGameRepository
    {
        public IReadOnlyList<VideoGameModel> GetAllVideoGames()
        {
            return MockVideoGameData.Games;
        }

        public VideoGameModel? GetVideoGameById(int id)
        {
            return MockVideoGameData.Games.FirstOrDefault(game => game.Id == id);
        }
    }
}
