using Microsoft.AspNetCore.Mvc;
using VideoGameCatalog.Server.Repositories;

namespace VideoGameCatalog.Server.Controllers;

[ApiController]
[Route("api/games")]
public class VideoGamesController : ControllerBase
{
    private readonly IVideoGameRepository _repo;

    public VideoGamesController(IVideoGameRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var games = await _repo.GetAllAsync();
        return Ok(games);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var game = await _repo.GetByIdAsync(id);
        if (game == null)
            return NotFound();
        return Ok(game);

    }
}