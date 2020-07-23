using System;
using DataAccess.Configurations;
using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.EntityContexts
{
    public class UserContext : IdentityDbContext<User>
    {
        private readonly IConfiguration _configuration;

        public UserContext(DbContextOptions<UserContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new UserConfiguration(_configuration));
            base.OnModelCreating(modelBuilder);
        }
    }
}