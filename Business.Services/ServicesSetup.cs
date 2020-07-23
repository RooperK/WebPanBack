using System.IO;
using AutoMapper;
using Business.Contracts;
using Business.Interfaces;
using Business.Mapping;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Business.Services
{
    public class ServicesSetup
    {
        public static void ConfigureServices(IServiceCollection sevices, IConfiguration configuration, 
            out Profile[] outProfiles)
        {
            outProfiles = new Profile[]
            {
                new PanoramaDtoMapping(),
                new TileDtoMapping()
            };
            sevices
                .AddTransient<IPanoramaService, PanoramaService>()
                .AddTransient<ITileService, TileService>()
                .AddTransient<IUserService, UserService>();
        }
    }
}