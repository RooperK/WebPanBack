using System.Collections.Generic;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Configurations
{
    public class ContextConfiguration<TE> : IContextConfiguration<TE> where TE: class
    {
        public IEnumerable<IConfigurationSection> Configuration { get; }

        public ContextConfiguration(IConfiguration configuration)
        {
            var type = typeof(TE).Name;
            Configuration = configuration
                .GetSection("Settings")
                .GetSection("Config")
                .GetSection(type)
                .GetChildren();
        }

        public void Configure(EntityTypeBuilder<TE> builder)
        {
            foreach (var section in Configuration) builder.Property(section.Key).HasMaxLength(int.Parse(section.Value));
        }
    }
}