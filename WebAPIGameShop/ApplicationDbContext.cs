using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAPIGameShop.Entidades;

namespace WebAPIGameShop
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<GameShopVideogames>().HasKey(gv => new { gv.GameShopId, gv.VideogameId });
        }
        public DbSet<GameShop> Games { get; set; }

        public DbSet<VideoGame> VideoGames { get; set; }
        public DbSet<Opinion> Opinions { get; set; }
        public DbSet<GameShopVideogames> GameShopVideogames { get; set; }
    }
}
