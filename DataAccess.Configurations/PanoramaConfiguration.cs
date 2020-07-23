using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Configurations
{
    public class PanoramaConfiguration : BaseConfiguration<Panorama>, IPanoramaConfiguration
    {
        public PanoramaConfiguration(IConfiguration configuration) : base(configuration)
        {
        }

        public new void Configure(EntityTypeBuilder<Panorama> builder)
        {
            builder
                .HasMany(collection => collection.Tiles)
                .WithOne(tile => tile.Panorama)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasKey(panorama => panorama.Id);
            base.Configure(builder);
        }
    }
}