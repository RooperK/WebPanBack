using System.Collections.Generic;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Server
{
    public class ControllerSetup
    {
        public static void ConfigureControllers(IServiceCollection services, IEnumerable<Profile> profiles,
            IConfiguration configuration)
        {
            services
                .AddSingleton(new MapperConfiguration(profile =>
                {
                    profile.AddProfiles(profiles);
                    profile.AddMaps("Webapp");
                }).CreateMapper())
                .AddControllers();
        }
    }
}