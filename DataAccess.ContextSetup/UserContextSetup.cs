using System;
using System.IO;
using DataAccess.EntityContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DataAccess.ContextSetup
{
    public class UserContextSetup : IDesignTimeDbContextFactory<UserContext>
    {
        public UserContext CreateDbContext(string[] args)
        {
            var path = Directory.GetCurrentDirectory(); 
            path = path.Substring(0, path.IndexOf("DataAccess.ContextSetup", StringComparison.Ordinal));
            var config = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("appsettings.json")
                .Build();
            var optionsBuilder = new DbContextOptionsBuilder<UserContext>();
            optionsBuilder
                .UseNpgsql(config.GetConnectionString("Users"),options =>
                {
                    options.MigrationsAssembly("DataAccess.ContextSetup");
                });

            return new UserContext(optionsBuilder.Options, config);
        }
    }
}