using DataAccess.Models;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Configurations
{
    public class MarkerConfiguration : BaseConfiguration<Marker>
    {
        public MarkerConfiguration(IConfiguration configuration) : base(configuration)
        {
        }
    }
}