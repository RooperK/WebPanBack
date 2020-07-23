using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Configurations
{
    public class TileConfiguration : BaseConfiguration<Tile>, ITileConfiguration
    {
        public TileConfiguration(IConfiguration configuration) : base(configuration)
        {}

        public new void Configure(EntityTypeBuilder<Tile> builder)
        {
            builder
                .HasOne(tile => tile.Panorama)
                .WithMany(collection => collection.Tiles)
                .OnDelete(DeleteBehavior.SetNull);
            base.Configure(builder);
        }
    }
}