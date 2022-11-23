using AutoMapper;
using WebAPIGameShop.DTOs;
using WebAPIGameShop.Entidades;

namespace WebAPIGameShop.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<GameShopCreacionDTO, GameShop>();
            CreateMap<GameShop, GameShopDTO>();
            CreateMap<GameShop, GameShopDTOconVideoGames>()
                .ForMember(gameshopDTO => gameshopDTO.VideoGames, opciones => opciones.MapFrom(MapGamesShopVideogames));
            CreateMap<VideoGameCreacionDTO, VideoGame>().ForMember(VideoGame => VideoGame.GameShopVideogames, opciones
                => opciones.MapFrom(MapGamesShopVideogames));
            CreateMap<VideoGame, VideoGameDTO>();
            CreateMap<VideoGame, VideoGamesDTOconGameShop>().ForMember(VideoGameDTO => VideoGameDTO.GameShops, opciones =>
            opciones.MapFrom(MapVideogameDTOGameShop));
            CreateMap<OpinionCreacionDTO, Opinion>();
            CreateMap<Opinion, OpinionDTO>();
        }
        private List<VideoGameDTO> MapGamesShopVideogames(GameShop gameShop, GameShopDTO gameShopDTO)
        {
            var resultado = new List<VideoGameDTO>();

            if(gameShop.GameShopVideogames == null) { return resultado; }

            foreach ( var gameShopVideogames in gameShop.GameShopVideogames)
            {
                resultado.Add(new VideoGameDTO()
                {
                    Id = gameShopVideogames.VideogameId,
                    Nombre = gameShopVideogames.VideoGame.Nombre,
                    Genero = gameShopVideogames.VideoGame.Genero

                });
            }
            return resultado;
        }
        private List<GameShopDTO> MapVideogameDTOGameShop(VideoGame videoGame, VideoGameDTO videoGameDTO)
        {
            var resultado = new List<GameShopDTO>();
            if(videoGame.GameShopVideogames == null)
            {
                return resultado;
            }
            foreach( var videogameShop in videoGame.GameShopVideogames)
            {
                resultado.Add(new GameShopDTO()
                {
                    id = videogameShop.GameShopId,
                    Nombre = videogameShop.GameShop.Nombre
                });
            }

            return resultado;
        }

        private List<GameShopVideogames> MapGamesShopVideogames(VideoGameCreacionDTO videoGameCreacionDTO, VideoGame videoGame)
        {
            var resultado = new List<GameShopVideogames>();
            if(videoGameCreacionDTO.GamesIds == null) { return resultado; }

            foreach(var gameId in videoGameCreacionDTO.GamesIds)
            {
                resultado.Add(new GameShopVideogames() { GameShopId = gameId });
            }
            return resultado;
        }
    }
}
