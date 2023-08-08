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

    public class PortfolioController : ControllerBase
    {
        private readonly IPortfolio? _iportfolio;
        public PortfolioController(IPortfolio iportfolio)
        {
            _iportfolio = iportfolio;
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreatePortfolio([FromForm] Portfolio_DTO portfolios)
        {
            const string A = "Portfolio Created Successfuly";
            try
            {
                if (ModelState.IsValid)
                {
                    var portfolio = await _iportfolio!.CreatePortfolio(portfolios);
                    switch (portfolio)
                    {
                        case (A):
                            return Ok(portfolio);
                        default:
                            return BadRequest(portfolio);
                    }
                }
                return BadRequest("Please fill all required feild");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("getall")]
        public async Task<ActionResult> GetPortfolios()
        {
            try
            {
                var portfolios = await _iportfolio!.GetPortfolios();
                if (portfolios.ToList().Count == 0)
                {
                    throw new Exception("Non Found");
                }
                return Ok(portfolios);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeletePortfolio([FromRoute] string id)
        {
            try
            {
                var portfolio = await _iportfolio!.DeletePortfolio(id);
                if (portfolio == "Deleted")
                {
                    return Ok("Deleted");
                }
                if (portfolio == "Portfolio not found")
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

        [HttpGet("get/{username}")]
        public async Task<ActionResult> GetPortfolio([FromRoute] string Username)
        {
            try
            {
                var portfolio = await _iportfolio!.GetPortfolio(Username);
                if (portfolio == null)
                {
                    return NotFound();
                }
                return Ok(portfolio);
            }
            catch (System.Exception)
            {
                return null!;
            }
        }

        [HttpGet("getsingle/{Id}")]
        public async Task<ActionResult> GetAPortfolio([FromRoute] string Id)
        {
            try
            {
                var portfolio = await _iportfolio!.GetAPortfolio(Id);
                if (portfolio == null!)
                {
                    return NotFound();
                }
                return Ok(portfolio);
            }
            catch (System.Exception)
            {
                return null!;
            }
        }

        [HttpPatch("update/{id}")]
        public async Task<ActionResult> UpdatePortfolio([FromForm] Portfolio_DTO portfolios, [FromRoute] string id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var portfolio = await _iportfolio!.UpdatePortfolio(id, portfolios);
                    if (portfolio == "Updated Successfully")
                    {
                        return Ok(portfolio);
                    }
                    return BadRequest(portfolio);
                }
                return BadRequest("An Error occured");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}