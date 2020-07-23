using System.Threading.Tasks;
using AutoMapper.Configuration;
using Business.Interfaces;
using DataAccess.Models;
using Microsoft.AspNetCore.Identity;

namespace Business.Services
{
    public class UserService : IUserService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public UserService(RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<bool> ContainsUserById(string id)
        {
            return id != null && await _userManager.FindByIdAsync(id) != null;
        }
    }
}