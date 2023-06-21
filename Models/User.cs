using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Backend.Models
{
    public class User : IdentityUser
    {
        public string? Role { get; set; } = "User";
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        
        // public List<Projects>? Project { get; set; }
    }
}