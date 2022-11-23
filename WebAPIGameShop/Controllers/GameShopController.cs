using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using WebAPIGameShop.Entidades;
using WebAPIGameShop.Migrations;
using WebAPIGameShop.Filtros;
using Microsoft.VisualBasic;
using System.Diagnostics;
using WebAPIGameShop.Services;

namespace WebAPIGameShop.Controllers
{
    [ApiController]
    [Route("api/gameshop")]
    public class GameShopController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IService service;
        private readonly ServiceTransient serviceTransient;
        private readonly ServiceScoped serviceScoped;
        private readonly ServiceSingleton serviceSingleton;
        private readonly ILogger<GameShopController> logger;

        public GameShopController(ApplicationDbContext dbContext, IService service,
          ServiceTransient serviceTransient, ServiceScoped serviceScoped,
          ServiceSingleton serviceSingleton, ILogger<GameShopController> logger)
        {
            this.dbContext = dbContext;
            this.service = service;
            this.serviceTransient = serviceTransient;
            this.serviceScoped = serviceScoped;
            this.serviceSingleton = serviceSingleton;
            this.logger = logger;
        }
        [HttpGet("GUID")]
        [ResponseCache(Duration = 10)]
        [ServiceFilter(typeof(FiltroDeAccion))]
        public ActionResult ObtenerGUID()
        {
            return Ok(new
            {
                GamesShopControllerTransient = serviceTransient.guid,
                ServiceA_Transient = service.GetTransient(),
                GamesShopControllerScoped = serviceScoped.guid,
                ServiceA_Scoped = service.GetScoped(),
                GamesShopControllerSingleton = serviceSingleton.guid,
                ServiceA_Singleton = service.GetSingleton()
            });
        }
        [HttpGet]//api/gameshop
        [HttpGet("listado")]//api/gamshop/listado
        [HttpGet("/listadoº")]//listado
        [ResponseCache(Duration = 15)]
        //[Authorize]
        //[ServiceFilter(typeof(FiltroDeAccion))]
        public async Task<ActionResult<List<GameShop>>> Get()
        {
            // critical, error, warning, information, debug, trace

            //throw new NotImplementedException();//con esto mandamos ver tooodo el log, hay otro tipo de excepciones
            logger.LogInformation("Se obtiene el listado de gameshop");
            logger.LogWarning("Mensaje de prueba warning");
            service.ejecutarJob();
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
                logger.LogError("No se encuentra el game");
                return NotFound();
            }
            return game;
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GameShop gameShop)
        {
            var existeGameMismoNombre = await dbContext.Games.AnyAsync(x => x.Nombre == gameShop.Nombre);

            if (existeGameMismoNombre)
            {
                return BadRequest("Ya existe un game con el nombre");
            }

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
