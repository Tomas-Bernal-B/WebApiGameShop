using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIGameShop.Entidades;

namespace WebAPIGameShop.Controllers
{
    [ApiController]
    [Route("api/videoGames")]
    public class VideoGamesController : ControllerBase

    {
        private readonly ApplicationDbContext dbContext;

        public VideoGamesController(ApplicationDbContext context)
        {
            this.dbContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<VideoGame>>> GetAll()
        {
            return await dbContext.VideoGames.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<VideoGame>> GetById(int id)
        {
            return await dbContext.VideoGames.FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult> Post(VideoGame videoGame)
        {
            var exiteGame = await dbContext.Games.AnyAsync(x => x.Id == videoGame.VideoGameId);
            if (!exiteGame)
            {
                return BadRequest("No existe el game con el id ingresado");
            }

            dbContext.Add(videoGame);
            await dbContext.SaveChangesAsync();
            return Ok();

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(VideoGame videoGame, int id)
        {
            var exist = await dbContext.VideoGames.AnyAsync(x => x.Id == id);

            if (!exist)
            {
                return NotFound("El juego especificado no existe");
            }

            if (videoGame.Id != id)
            {
                return BadRequest("El id de el Game no coincide con el establecido en el url");
            }

            dbContext.Update(videoGame);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.VideoGames.AnyAsync(x => x.Id == id);

            if (!exist)
            {
                return NotFound("El Game no fue encontrado");
            }

            dbContext.Remove(new VideoGame { Id = id });
            await dbContext.SaveChangesAsync();
            return Ok();

        }
    }

}
