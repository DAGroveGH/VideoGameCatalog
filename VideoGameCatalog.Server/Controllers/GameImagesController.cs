using Microsoft.AspNetCore.Mvc;
using VideoGameCatalog.Server.Services;

namespace VideoGameCatalog.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameImagesController : ControllerBase
{
    private readonly IRawgExternalService _rawgService;

    public GameImagesController(IRawgExternalService rawgService)
    {
        _rawgService = rawgService;
    }

    [HttpPost]
    public async Task<IActionResult> GetImages([FromBody] List<string> titles)
    {
        var result = await _rawgService.GetGameImagesAsync(titles);
        return Ok(result);
    }
}