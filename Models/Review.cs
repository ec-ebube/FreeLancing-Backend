using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class Review
    {
       public string? Id { get; set; }
       public string? Portfolio_Id { get; set; }
       public string? UorP_Id { get; set; }
       public int Rating { get; set; }
       public string? Comment { get; set; }
    }
}