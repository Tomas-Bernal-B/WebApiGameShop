using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPIGameShop.Validaciones;


namespace WebAPIGameShop.Entidades
{
    public class GameShop
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo nombre es requerido")]
        [StringLength(maximumLength: 25, ErrorMessage = "El campo {0} es requerido")]
        [PrimeraLetraMayuscula]

        public string Nombre { get; set; }
        public List<GameShopVideogames> GameShopVideogames { get; set; }

    }
}
