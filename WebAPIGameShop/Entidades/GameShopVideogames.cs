namespace WebAPIGameShop.Entidades
{
    public class GameShopVideogames
    {
        public int VideogameId { get; set; }
        public int GameShopId { get; set; }
        public int Orden { get; set; }

        public VideoGame VideoGame{ get; set; }
        public GameShop GameShop { get; set; }
    }
}
