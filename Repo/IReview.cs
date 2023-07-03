using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTO;
using Backend.Models;

namespace Backend.Repo
{
    public interface IReview
    {
        public Task<IEnumerable<Review>> GetReviews();
        public Task<Review> GetReview(string id);
        public Task<string> CreateReview(Review_DTO review);
        public Task<string> UpdateReview(string id, Review_DTO review);
        public Task<string> DeleteReview(string id);
    }
}