using Microsoft.AspNetCore.Identity;

namespace WebAPIGameShop.Entidades
{
    public class Opinion
    {
        public int id { get; set; }
        public string Contenido { get; set; }
        public int VideoGameID { get; set; }
        public VideoGame videoGame { get; set; }
        public string UsuarioId { get; set; }

        public IdentityUser Usuario { get; set; }
    }
}
