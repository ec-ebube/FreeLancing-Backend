using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTO
{
    public class Portfolio_DTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public DateTime DoB { get; set; }
        public string? UserName { get; set; }
        [NotMapped]
        public IFormFile? ProfilePhoto { get; set; }
        public string? ProfilePath { get; set; }
        [MaxLength(150)]
        public string? Bio { get; set; }
        public string? Skill { get; set; }
    }
}