using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class Project
    {
        public string? Id { get; set; }
        public string? Portfolio_Id { get; set; }
        public string? Title { get; set; }
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