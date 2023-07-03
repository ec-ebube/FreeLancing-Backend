using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTO;
using Backend.Models;
using Backend.Repo;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace Backend.Services
{
    public class ReviewServices : IReview
    {
        private readonly LE_dbContext? _leContext;
        public ReviewServices(LE_dbContext leContext)
        {
            _leContext = leContext;
        }

        public Task<string> CreateReview(Review_DTO review)
        {
            return NotImplementedException();
        }

        public Task<string> DeleteReview(string id)
        {
            return NotImplementedException();
        }

        public Task<IEnumerable<Review>> GetReviews()
        {
            return NotImplementedException();
        }

        public Task<Review> GetReview(string Id)
        {
            return NotImplementedException();
        }

        public Task<string> UpdateReview(string Id, Review_DTO review)
        {
            return NotImplementedException();
        }
        
    }
}