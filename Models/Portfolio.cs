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
        [Required]
        [StringLength(50, MinimumLength = 2)]
        [RegularExpression(@"^([A-Za-z-.']+)$", ErrorMessage = "format not accepted")]
        public string? FirstName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        [RegularExpression(@"^([A-Za-z-.']+)$", ErrorMessage = "format not accepted")]
        public string? LastName { get; set; }
        public DateTime DoB { get; set; }
        [NotMapped]
        public IFormFile? ProfilePhoto { get; set; }
        public string? ProfilePath { get; set; }
        [MaxLength(150)]
        public string? Bio { get; set; }
        public string? Skill { get; set; }
        public string? Category { get; set; }
        public string? Pricing { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}