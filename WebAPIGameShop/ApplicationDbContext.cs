using Microsoft.EntityFrameworkCore;
using WebAPIGameShop.Entidades;

namespace WebAPIGameShop
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<GameShop> Games { get; set; }

        public DbSet<VideoGame> VideoGames { get; set; }
    }
}
