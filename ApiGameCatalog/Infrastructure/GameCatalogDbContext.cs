using ApiGameCatalog.Entities;
using ApiGameCatalog.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

namespace ApiGameCatalog.Repositories
{
    public class GameCatalogDbContext : DbContext
    {
        public GameCatalogDbContext(DbContextOptions<GameCatalogDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PublisherMapping());
            modelBuilder.ApplyConfiguration(new GameMapping());
            modelBuilder.ApplyConfiguration(new UserMapping());
            
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Game> Game { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Publisher> Publisher { get; set; }

    }    
}
