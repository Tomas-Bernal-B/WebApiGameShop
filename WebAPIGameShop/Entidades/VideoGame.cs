namespace WebAPIGameShop.Entidades
{
    public class VideoGame
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Genero { get; set; }

        public int VideoGameId { get; set; }

        public GameShop GameShop { get; set; }
    }
}
