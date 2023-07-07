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

        [HttpPost]
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
    }
}