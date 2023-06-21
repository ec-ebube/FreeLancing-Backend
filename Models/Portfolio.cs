using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Backend.Models
{
    public class Portfolio : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DoB { get; set; }
        [NotMapped]
        public IFormFile? ProfilePhoto { get; set; }
        public string? ProfilePath { get; set; }
        [MaxLength(150)]
        public string? Bio { get; set; }
        public string? Skill { get; set; }
    }
}