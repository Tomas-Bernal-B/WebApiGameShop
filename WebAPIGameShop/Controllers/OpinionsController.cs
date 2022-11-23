using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIGameShop.DTOs;
using WebAPIGameShop.Entidades;

namespace WebAPIGameShop.Controllers
{
    [ApiController]
    [Route("api/videogames/{videoGameId:int}/opinons")]
    public class OpinionsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser> userManager;

        public OpinionsController(ApplicationDbContext dbContext, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.userManager = userManager;
        }
        [HttpGet]
        public async Task<ActionResult<List<OpinionDTO>>> Get(int videogameId)
        {
            var opiniones = await dbContext.Opinions.Where(x => x.VideoGameID == videogameId).ToListAsync();
            return mapper.Map<List<OpinionDTO>>(opiniones);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post(int videogameId, OpinionCreacionDTO opinionCreacionDTO)
        {
            var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();
            var email = emailClaim.Value;
            var usuario = await userManager.FindByEmailAsync(email);
            var usuarioId = usuario.Id;
            var existeGame = await dbContext.VideoGames.AnyAsync(x => x.Id == videogameId);
            if (!existeGame)
            {
                return NotFound();
            }

            var opinon = mapper.Map<Opinion>(opinionCreacionDTO);
            opinon.VideoGameID = videogameId;
            opinon.UsuarioId = usuarioId;
            dbContext.Add(opinon);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
