using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class Project
    {
        public Guid Id { get; set; }
        public string? Portfolio_Id { get; set; }
        public string? Description { get; set; }
        public IFormFile? ProjectImage { get; set; }
        public string? ProjectImagePath { get; set; }
        public IFormFile? ProjectVideo { get; set; }
        public string? ProjectVideoPath { get; set; }
        public Portfolio? Portfolio { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}