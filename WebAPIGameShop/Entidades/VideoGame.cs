using System.ComponentModel.DataAnnotations;
using WebAPIGameShop.Validaciones;

namespace WebAPIGameShop.Entidades
{
    public class VideoGame
    {
        public int Id { get; set; }
        [Required]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }
        [PrimeraLetraMayuscula]
        public string Genero { get; set; }

        public List<Opinion> Opinions { get; set; }
        public List<GameShopVideogames> GameShopVideogames { get; set; }
        
      
        
    }
}
