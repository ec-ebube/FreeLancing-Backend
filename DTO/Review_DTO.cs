using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTO
{
    public class Review_DTO
    {
        public string? Id { get; set; }
        public string? Portfolio_Id { get; set; }
        public string? UorP_Id { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
       public DateTime Created { get; set; }
       public DateTime Modified { get; set; }
    }
}