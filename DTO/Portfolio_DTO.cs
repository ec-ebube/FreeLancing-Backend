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
        [Required]
        [StringLength(50, MinimumLength = 2)]
        [RegularExpression(@"^([A-Za-z-.']+)$", ErrorMessage = "format not accepted")]
        public string? FirstName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        [RegularExpression(@"^([A-Za-z-.']+)$", ErrorMessage = "format not accepted")]
        public string? LastName { get; set; }
        [Required]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$", ErrorMessage = "Invalid Email pattern.")]
        [MaxLength(50)]
        public string? Email { get; set; }
        public DateTime DoB { get; set; }
        [MaxLength(100)]
        [StringLength(100, MinimumLength = 6)]
        public string? Password { get; set; }
        public string? UserName { get; set; }
        [NotMapped]
        public IFormFile? ProfilePhoto { get; set; }
        public string? ProfilePath { get; set; }
        [MaxLength(150)]
        public string? Bio { get; set; }
        public string? Skill { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}