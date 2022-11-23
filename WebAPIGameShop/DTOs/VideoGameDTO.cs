using WebAPIGameShop.Validaciones;

namespace WebAPIGameShop.DTOs
{
    public class VideoGameDTO
    {
        public int Id { get; set; }
       
        public string Nombre { get; set; }
  
        public string Genero { get; set; }
    
        //public List<OpinionDTO> Opinions { get; set; }
    }
}
