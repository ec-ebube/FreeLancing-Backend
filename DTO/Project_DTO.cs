using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;

namespace Backend.DTO
{
    public class Project_DTO
    {
        // public Guid Id { get; set; }
        public string? Portfolio_Id { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 6)]
        [RegularExpression(@"^([A-Za-z-.']+)$", ErrorMessage = "format not accepted")]
        public string? Title { get; set; }
         [Required]
        [StringLength(40, MinimumLength = 6)]
        [RegularExpression(@"^([A-Za-z-.']+)$", ErrorMessage = "format not accepted")]
        public string? Description { get; set; }
        [NotMapped]
        public IFormFile? ProjectImage { get; set; }
        public string? ProjectImagePath { get; set; }
        [NotMapped]
        public IFormFile? ProjectVideo { get; set; }
        public string? ProjectVideoPath { get; set; }
        public Portfolio? Portfolio { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}