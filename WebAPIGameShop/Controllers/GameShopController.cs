using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using WebAPIGameShop.Entidades;

using WebAPIGameShop.Filtros;
using Microsoft.VisualBasic;
using System.Diagnostics;
using WebAPIGameShop.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace WebAPIGameShop.Controllers
{
    [ApiController]
    [Route("api/gameshop")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GameShopController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public GameShopController(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
       [HttpGet]//api/gameshop
        [AllowAnonymous]
        
        public async Task<ActionResult<List<GameShopDTO>>> Get()
        {
            var gameshop = await dbContext.Games.ToListAsync();
            return mapper.Map<List<GameShopDTO>>(gameshop);
        }
        [HttpGet("{id;int}")]
        public async Task<ActionResult<GameShopDTOconVideoGames>> Get(int id)
        {
            var gameshop = await dbContext.Games.
                Include(gameshopDB => gameshopDB.GameShopVideogames).ThenInclude(gameshopVideogame => gameshopVideogame.VideoGame)
                .FirstOrDefaultAsync(x => x.Id == id);
            if(gameshop == null)
            {
                return NotFound();
            }
            return mapper.Map<GameShopDTOconVideoGames>(gameshop);
        }
        
        [HttpGet("{nombre}")]
        public async Task<ActionResult<List<GameShopDTO>>> Get(string nombre)
        {
            var game =  await dbContext.Games.Where(x => x.Nombre.Contains(nombre)).ToListAsync();    
            
            return mapper.Map<List<GameShopDTO>>(game);
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GameShopCreacionDTO gameShopDTO)
        {
            var existeGameMismoNombre = await dbContext.Games.AnyAsync(x => x.Nombre == gameShopDTO.Nombre);

            if (existeGameMismoNombre)
            {
                return BadRequest("Ya existe un game con el nombre");
            }

            var gameshop = mapper.Map<GameShop>(gameShopDTO);

            dbContext.Add(gameShopDTO);
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
