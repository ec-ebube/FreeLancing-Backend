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

        public async Task<string> CreateReview(Review_DTO review)
        {
            try
            {
                var newrv = new Review();
                var id = Guid.NewGuid();
                newrv.Id = id.ToString();
                newrv.Portfolio_Id = review.Portfolio_Id;
                newrv.UorP_Id = review.UorP_Id;
                newrv.Rating = review.Rating;
                newrv.Comment = review.Comment;
                newrv.Created = DateTime.Now;
                newrv.Modified = DateTime.Now;

                await _leContext!.Reviews.AddAsync(newrv);
                _leContext!.SaveChanges();

                return "Successful";
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> DeleteReview(string id)
        {
            try
            {
                var review = await _leContext!.Reviews.FindAsync(id);
                if (review == null)
                {
                    return "Not Found";
                }

                _leContext!.Reviews.Remove(review);
                _leContext!.SaveChanges();

                return "deleted";
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<IEnumerable<Review>> GetReviews()
        {
            try
            {
                var review = await _leContext!.Reviews.OrderByDescending(x => x.Created).ToListAsync();
                if (review == null)
                {
                    return null!;
                }
                return review;
            }
            catch (System.Exception)
            {
                return null!;
            }
        }

        public Task<Review> GetReview(string Id)
        {
            throw new NotImplementedException();
        }

        public async Task<string> UpdateReview(string Id, Review_DTO review)
        {
            try
            {
                var editrv = await _leContext!.Reviews.FindAsync(Id);
                editrv!.Rating = review.Rating;
                editrv.Comment = review.Comment;
                editrv.Modified = DateTime.Now;

                _leContext!.Reviews.Attach(editrv);
                _leContext!.SaveChanges();
                return "Changed";
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
        }

    }
}