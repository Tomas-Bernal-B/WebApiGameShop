using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using WebAPIGameShop.Entidades;
using WebAPIGameShop.Migrations;

namespace WebAPIGameShop.Controllers
{
    [ApiController]
    [Route("api/gameshop")]
    public class GameShopController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public GameShopController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet("listado")]//api/gamshop/listado
        [HttpGet("/listadoº")]//listado
        public async Task<ActionResult<List<GameShop>>> Get()
        {
            return await dbContext.Games.Include(x => x.videoGames).ToListAsync();
        }

        [HttpGet("primero")] //api/gameshop/primero
        public async Task<ActionResult<GameShop>> PrimerGame([FromHeader] int valor, [FromQuery] string game,
            [FromQuery] int gameid)
        {
            return await dbContext.Games.FirstOrDefaultAsync();
        }
        [HttpGet("primero2")]//api/gameshop/primero2
        public ActionResult<GameShop> PrimerPeliculaD()
        {
            return new GameShop() { Nombre = "DOS" };
        }
        [HttpGet("{nombre}")]
        public async Task<ActionResult<GameShop>> Get(string nombre)
        {
            var game =  await dbContext.Games.FirstOrDefaultAsync(x => x.Nombre.Contains(nombre));    
            if(game == null)
            {
                return NotFound();
            }
            return game;
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GameShop gameShop)
        {
            dbContext.Add(gameShop);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(GameShop gameShop, int id)
        {
            if (gameShop.Id != id)
            {
                return BadRequest("El id del game no coicide con el establecido en el url");
            }

            dbContext.Update(gameShop);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delate(int id)
        {
            var exist = await dbContext.VideoGames.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound("El Recurso no fue encontrado.");
            }

            dbContext.Remove(new VideoGame { Id = id });
            await dbContext.SaveChangesAsync();
            return Ok();
        }

    
    }
}
