using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDbService;

namespace MongoDB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistController : ControllerBase
    {
        private readonly IMongoService _mongoService;
        public PlaylistController(IMongoService mongoService)
        {
            this._mongoService = mongoService;
        }

        [HttpGet]
        public async Task<List<Playlist>> Get()
        {
            return await _mongoService.GetAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Post(Playlist playlist)
        {
            await _mongoService.CreateAsync(playlist);
            return CreatedAtAction(nameof(Get), new { id = playlist.Id }, playlist);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AddToPlaylist(string id, [FromBody] string movieId)
        {
            await _mongoService.AddToPlaylistAsync(id, movieId);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _mongoService.DeleteAsync(id);
            return NoContent();
        }
    }
}
