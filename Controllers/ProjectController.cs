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
    public class ProjectController : ControllerBase
    {
        private readonly IProject? _iproject;
        public ProjectController(IProject iproject)
        {
            _iproject = iproject;
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateProject([FromForm] Project_DTO projects)
        {
            const string A = "Project Posted";
            try
            {
                if (ModelState.IsValid)
                {
                    var project = await _iproject!.CreateProject(projects);
                    switch (project)
                    {
                        case (A):
                            return Ok(project);
                        default:
                            return BadRequest(project);
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