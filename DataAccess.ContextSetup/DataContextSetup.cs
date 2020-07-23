using System;
using System.IO;
using DataAccess.EntityContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DataAccess.ContextSetup
{
    public class DataContextSetup : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var path = Directory.GetCurrentDirectory();
            path = path.Substring(0, path.IndexOf("DataAccess.ContextSetup", StringComparison.Ordinal));
            var config = new ConfigurationBuilder()
                .SetBasePath($"{path}WebApp")
                .AddJsonFile("appsettings.json")
                .Build();
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder
                .UseNpgsql(config.GetConnectionString("Data"), options =>
                {
                    options.MigrationsAssembly("DataAccess.ContextSetup");
                });

            return new DataContext(optionsBuilder.Options, config);
        }
    }
}