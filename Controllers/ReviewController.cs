using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTO;
using Backend.Repo;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : Controller
    {
        private readonly IReview? _ireview;
        public ReviewController(IReview ireview)
        {
            _ireview = ireview;
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateReview([FromForm] Review_DTO Reviews)
        {
            try
            {
                const string A = "Successful";
                if (ModelState.IsValid)
                {
                    var review = await _ireview!.CreateReview(Reviews);
                    switch (review)
                    {
                        case (A):
                            return Ok(review);
                        default:
                            return BadRequest(review);
                    }
                }
                return BadRequest();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getall")]
        public async Task<ActionResult> GetReviews()
        {
            try
            {
                var reviews = await _ireview!.GetReviews();
                if (reviews.ToList().Count == 0)
                {
                    return NotFound("Non Found");
                }
                return Ok(reviews);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}