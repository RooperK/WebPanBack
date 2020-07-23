using System;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Models
{
    public class User : IdentityUser
    {
        public string Avatar { get; set; }
        public bool IsExternal { get; set; }
    }
}