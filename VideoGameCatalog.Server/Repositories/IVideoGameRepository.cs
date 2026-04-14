using VideoGameCatalog.Shared.Models;

namespace VideoGameCatalog.Server.Repositories;

public interface IVideoGameRepository
{
    Task<IReadOnlyList<VideoGameModel>> GetAllAsync();
}