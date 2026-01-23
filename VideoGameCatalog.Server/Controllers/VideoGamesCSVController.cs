using Microsoft.AspNetCore.Mvc;
using VideoGameCatalog.Server.Repositories;
using VideoGameCatalog.Shared.Models;

namespace VideoGameCatalog.Server.Controllers
{
    [ApiController]
    [Route("api/videogamescsv")]
    public class VideoGamesCSVController : ControllerBase
    {
        private readonly VideoGameCSVRepository _videoGameCsvRepository;
        public VideoGamesCSVController(VideoGameCSVRepository videoGameCsvRepository)
        {
            _videoGameCsvRepository = videoGameCsvRepository;
        }

        [HttpGet]
        public IActionResult GetAllVideoGamesCSV()
        {
            return Ok(_videoGameCsvRepository.GetAllVideoGames());
        }

        [HttpGet("{id:int}")]
        public IActionResult GetVideoGameById(int id)
        {
            return _videoGameCsvRepository.GetVideoGameById(id) is VideoGameModel videoGame
                ? Ok(videoGame)
                : NotFound();
        }
    }
}
