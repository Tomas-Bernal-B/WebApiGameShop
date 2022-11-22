using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIGameShop.Entidades
{
    public class GameShop
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo nombre es requerido")]
        [StringLength(maximumLength: 25, ErrorMessage = "El campo {0} es requerido")]

        public string Nombre { get; set; }

        [Range(1900, 2022, ErrorMessage = "El campo estreno no esta dentro del rango")]
        [NotMapped]
        public int lanzamiento{ get; set; }

        public List<VideoGame> videoGames { get; set; }
    }
}
