using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIGameShop.DTOs;
using WebAPIGameShop.Entidades;


namespace WebAPIGameShop.Controllers
{
    [ApiController]
    [Route("api/videoGames")]
    public class VideoGamesController : ControllerBase

    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public VideoGamesController(ApplicationDbContext context, IMapper mapper)
        {
            this.dbContext = context;
            this.mapper = mapper;
        }

        [HttpGet("/listado")]// /listado
        public async Task<ActionResult<List<VideoGame>>> Get()
        {
            return await dbContext.VideoGames.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<VideoGamesDTOconGameShop>> GetById(int id)
        {
           var videogames = await dbContext.VideoGames.Include(x => x.GameShopVideogames).
                ThenInclude( videogaemedb => videogaemedb.GameShop).FirstOrDefaultAsync(x => x.Id == id);
            return mapper.Map<VideoGamesDTOconGameShop>(videogames);
        }

        [HttpPost]
        public async Task<ActionResult> Post(VideoGameCreacionDTO videoGameCreacionDTO)
        {
            if(videoGameCreacionDTO.GamesIds == null)
            {
                return BadRequest("No se puede crear un videogame sin Gameshop");
            }
            var gamshopIds = await dbContext.VideoGames.Where(x => videoGameCreacionDTO.GamesIds.Contains(x.Id)).Select(x => x.Id).ToListAsync();
            if(videoGameCreacionDTO.GamesIds.Count != gamshopIds.Count)
            {
                return BadRequest("No existe uno de los games enviados");
            }
            var videogame = mapper.Map<VideoGame>(videoGameCreacionDTO);
            if (videogame.GameShopVideogames != null)
            {
                for(int i=0; i< videogame.GameShopVideogames.Count; i++)
                {
                    videogame.GameShopVideogames[i].Orden = i;
                }
            }
            dbContext.Add(videogame);
            await dbContext.SaveChangesAsync();
            return Ok();

        }

       
    }

}
