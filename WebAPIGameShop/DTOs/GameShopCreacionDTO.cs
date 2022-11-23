using System.ComponentModel.DataAnnotations;
using WebAPIGameShop.Validaciones;

namespace WebAPIGameShop.DTOs
{
    public class GameShopCreacionDTO
    {
        [Required(ErrorMessage = "El campo nombre es requerido")]
        [StringLength(maximumLength: 25, ErrorMessage = "El campo {0} es requerido")]
        [PrimeraLetraMayuscula]

        public string Nombre { get; set; }
    }
}
