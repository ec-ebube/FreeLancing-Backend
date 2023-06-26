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
            catch (System.Exception  ex)
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

    }
}