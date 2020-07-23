using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Configurations
{
    public class BaseConfiguration<TE> : ContextConfiguration<TE> where TE : Base<string>
    {
        public BaseConfiguration(IConfiguration configuration) : base(configuration)
        {
        }

        protected new void Configure(EntityTypeBuilder<TE> builder)
        {
            builder
                .Property(b => b.Id)
                .HasValueGenerator<GuidGenerator>()
                .ValueGeneratedOnAdd();
        }
    }
}