using System;
using DataAccess.EntityContexts;
using DataAccess.Interfaces;
using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.ContextSetup
{
    public class ContextSetup
    {
        public static void ConfigureDbContext(IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDbContext<DataContext>(options => options
                    .UseNpgsql(configuration
                        .GetConnectionString("Data"), o
                        =>
                    {
                        o.MigrationsAssembly("DataAccess.ContextSetup");
                        o.EnableRetryOnFailure(5, TimeSpan.FromMilliseconds(10), 
                            null!);
                    }))
                .AddTransient<IPanoramaRepository, PanoramaRepository>()
                .AddTransient<ITileRepository, TileBinaryRepository>()
                .AddTransient<ITourRepository, TourRepository>()
                .AddTransient<IMarkerRepository, MarkerRepository>()
                .AddDbContext<UserContext>(options => options
                    .UseNpgsql(configuration.GetConnectionString("Users"), o
                        =>
                    {
                        o.MigrationsAssembly("DataAccess.ContextSetup");
                        o.EnableRetryOnFailure(5, TimeSpan.FromMilliseconds(10), 
                            null!);
                    }))
                .AddDefaultIdentity<User>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<UserContext>();
        }
    }
}