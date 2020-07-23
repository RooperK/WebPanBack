using DataAccess.Configurations;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.EntityContexts
{
    public class DataContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DataContext(DbContextOptions<DataContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }
        
        public DbSet<Tile> Tiles { get; set; }
        public DbSet<Marker> Markers { get; set; }
        public DbSet<Panorama> Panoramas { get; set; }
        public DbSet<Tour> Tours { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new TileConfiguration(_configuration))
                .ApplyConfiguration(new PanoramaConfiguration(_configuration));
            base.OnModelCreating(modelBuilder);
        }
    }
}