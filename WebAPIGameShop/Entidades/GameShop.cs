namespace WebAPIGameShop.Entidades
{
    public class GameShop
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public List<VideoGame> videoGames { get; set; }
    }
}
