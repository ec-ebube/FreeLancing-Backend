using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Repo;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
   [ApiController]
    [Route("api/[Controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILogin? _ilogin;
        public LoginController(ILogin? ilogin)
        {
            _ilogin = ilogin;
        }

        [HttpPost("loguser")]
        public async Task<ActionResult> login([FromForm] string Email, [FromForm] string password)
        {
            try
            {
                var user = await _ilogin!.Login(Email, password);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("logport")]
        public async Task<ActionResult> logport([FromForm] string Email, [FromForm] string password)
        {
            try
            {
                var portfolio = await _ilogin!.LogP(Email, password);
                if (portfolio == null)
                {
                    return NotFound();
                }
                return Ok(portfolio);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}