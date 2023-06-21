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

    public class UserController : ControllerBase
    {
        private readonly IUser? _iuser;
        public UserController(IUser iuser)
        {
            _iuser = iuser;
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateUser([FromForm] User_DTO users)
        {
            const string A = "User Created Successfully";
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _iuser!.CreateUser(users);
                    switch (user)
                    {
                        case (A):
                            return Ok(user);
                        default:
                            return BadRequest(user);
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
        public async Task<ActionResult> getUsers()
        {
            try
            {
                var users = await _iuser!.GetUsers();
                if (users.ToList().Count == 0)
                {
                    throw new Exception("No User found");
                }
                return Ok(users);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteUser([FromRoute] string id)
        {
            try
            {
                var user = await _iuser!.DeleteUser(id);
                if (user == "User Deleted")
                {
                    return Ok("Deleted");
                }
                if (user == "User Not Found")
                {
                    return NotFound();
                }
                return BadRequest();

            }
            catch (System.Exception  ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("update/{id}")]
        public async Task<ActionResult> UpdateUser([FromForm] User_DTO users, [FromRoute] string id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _iuser!.UpdateUser(id, users);
                    if (user == "Edited Successfully")
                    {
                        return Ok(user);
                    }
                    return BadRequest(user);
                }
                return BadRequest("Incorrect Input");
            }
            catch (System.Exception ex)
            {
               return BadRequest(ex.Message);
            }
        }

        [HttpGet("get/{id}")]
        public async Task<ActionResult> getSingleUser([FromRoute] string Id)
        {
            var user = await _iuser!.GetUser(Id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}