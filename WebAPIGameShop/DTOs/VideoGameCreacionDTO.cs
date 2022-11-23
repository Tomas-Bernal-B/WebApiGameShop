using WebAPIGameShop.Validaciones;

namespace WebAPIGameShop.DTOs
{
    public class VideoGameCreacionDTO
    {
        public int Id { get; set; }
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }
        [PrimeraLetraMayuscula]
        public string Genero { get; set; }
        public List<int> GamesIds { get; set; }
    }
}
