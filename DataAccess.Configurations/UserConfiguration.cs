using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Configurations
{
    public class UserConfiguration : ContextConfiguration<User>, IUserConfiguration
    {
        public UserConfiguration(IConfiguration configuration) : base(configuration)
        {}
    }
}