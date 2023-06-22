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
            const string A = "Protfolio Created Successfully";
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

    }
}