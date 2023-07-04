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


        [HttpPatch("update/{id}")]
        public async Task<ActionResult> UpdateReview([FromForm] Review_DTO reviews, [FromRoute] string id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var review = await _ireview!.UpdateReview(id, reviews);
                    if (review == "Changed")
                    {
                        return Ok(review);
                    }
                    return BadRequest(review);
                }
                return BadRequest();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult>  DeleteReview([FromRoute] string id)
        {
            try
            {
                var review = await _ireview!.DeleteReview(id);
                if (review == "deleted")
                {
                    return Ok("Deleted");
                }
                if (review == "Not Found")
                {
                    return NotFound();
                }
                return BadRequest();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}