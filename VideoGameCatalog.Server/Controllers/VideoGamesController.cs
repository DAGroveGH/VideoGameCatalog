using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VideoGameCatalog.Server.Repositories;
using VideoGameCatalog.Shared.Models;

namespace VideoGameCatalog.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VideoGamesController : ControllerBase
    {
        private readonly IVideoGameRepository _videoGameRepository;
        public VideoGamesController(IVideoGameRepository videoGameRepository)
        {
            _videoGameRepository = videoGameRepository;
        }

        [HttpGet]
        public IActionResult GetAllVideoGames()
        {
            return Ok(_videoGameRepository.GetAllVideoGames());
        }

        [HttpGet("{id:int}")]
        public IActionResult GetVideoGameById(int id)
        {
            return _videoGameRepository.GetVideoGameById(id) is VideoGameModel videoGame
                ? Ok(videoGame)
                : NotFound();
            //var videoGame = _videoGameRepository.GetVideoGameById(id);
            //if (videoGame == null)
            //{
            //    return NotFound();
            //}
            //return Ok(videoGame);
        }
        //[HttpGet]
        //public async Task<IActionResult> GetAllVideoGames()
        //{
        //    var videoGames = await _videoGameRepository.GetAllVideoGamesAsync();
        //    return Ok(videoGames);
        //}

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetVideoGameById(int id)
        //{
        //    var videoGame = await _videoGameRepository.GetVideoGameByIdAsync(id);
        //    if (videoGame == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(videoGame);
        //}

        //[HttpPost]
        //public async Task<IActionResult> AddVideoGame([FromBody] VideoGame videoGame)
        //{
        //    var createdVideoGame = await _videoGameRepository.AddVideoGameAsync(videoGame);
        //    return CreatedAtAction(nameof(GetVideoGameById), new { id = createdVideoGame.Id }, createdVideoGame);
        //}
    }
}
