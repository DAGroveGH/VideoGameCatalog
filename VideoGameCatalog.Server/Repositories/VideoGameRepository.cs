using Mapster;
using Microsoft.EntityFrameworkCore;
using VideoGameCatalog.Server.Data;
using VideoGameCatalog.Shared.Models;

namespace VideoGameCatalog.Server.Repositories;

public class VideoGameRepository : IVideoGameRepository
{
    private readonly ApplicationDbContext _db;

    public VideoGameRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IReadOnlyList<VideoGameModel>> GetAllAsync()
    {
        var items = await _db.CollectionItems
            .Include(x => x.Platform)
            .Include(x => x.Publisher)
            .Include(x => x.Developer)
            .Include(x => x.Genre)
            .ToListAsync();

        return items.Adapt<List<VideoGameModel>>();
    }

    public async Task<VideoGameModel?> GetByIdAsync(int id)
    {
        var item = await _db.CollectionItems
            .FirstOrDefaultAsync(x => x.Id == id);

        return item?.Adapt<VideoGameModel>();
    }
}