using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;

namespace Backend.DTO
{
    public class User_DTO
    {
        public string? Email { get; set; }
        public string? Role { get; set; } = "User";
        public string? Password { get; set; }
        // public string? Bio { get; set; }
        // public string? Skill { get; set; }
    }
}