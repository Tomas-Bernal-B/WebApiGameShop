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
        [HttpGet]//api/gamshop
        [HttpGet("listado")]//api/gamshop/listado
        [HttpGet("/listadoº")]
        public async Task<ActionResult<List<GameShop>>> Get()
        {
            return await dbContext.Games.Include(x => x.videoGames).ToListAsync();
        }

        [HttpGet("primero")] //api/gameshop/primero
        public async Task<ActionResult<GameShop>> PrimerGame()
        {
            return await dbContext.Games.FirstOrDefaultAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<GameShop>> Get(int id)
        {
            return await dbContext.Games.FirstOrDefaultAsync(x => x.Id == id);    
        }
        [HttpPost]

        public async Task<ActionResult> Post(GameShop gameShop)
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
