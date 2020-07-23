using System.Collections.Generic;
using Business.Services;
using DataAccess.ContextSetup;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Server
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            ContextSetup.ConfigureDbContext(services, _configuration);
            ServicesSetup.ConfigureServices(services, _configuration, out var maps);
            ControllerSetup.ConfigureControllers(services, maps, _configuration);
            if (_env.IsDevelopment())
            {
                services.AddSwaggerGen(c =>
                    {
                        c.SwaggerDoc("v1", new OpenApiInfo
                        {
                            Title = "My API",
                            Version = "v1"
                        });
                        /*c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                        {
                            Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n" +
                                          "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n" +
                                          "Example: 'Bearer 12345abcdef'",
                            Name = "Authorization",
                            In = ParameterLocation.Header,
                            Type = SecuritySchemeType.ApiKey,
                            Scheme = JwtBearerDefaults.AuthenticationScheme
                        });

                        c.AddSecurityRequirement(
                            new OpenApiSecurityRequirement
                            {
                                {
                                    new OpenApiSecurityScheme
                                    {
                                        Reference = new OpenApiReference
                                        {
                                            Type = ReferenceType.SecurityScheme,
                                            Id = "Bearer"
                                        },
                                        Scheme = "oauth2",
                                        Name = "Bearer",
                                        In = ParameterLocation.Header
                                    },
                                    new List<string>()
                                }
                            });*/
                    }
                );
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });
            }
            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());
            //app.UseHttpsRedirection();
            app.UseRouting();
            /*app.UseAuthentication();
            app.UseAuthorization();*/
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
