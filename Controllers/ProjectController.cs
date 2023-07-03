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

        [HttpGet("getall")]
        public async Task<ActionResult> GetProjects()
        {
            try
            {
                var projects = await _iproject!.GetProjects();
                if (projects.ToList().Count == 0)
                {
                    return NotFound("Non Found");
                }
                return Ok(projects);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteProject([FromRoute] string id)
        {
            try
            {
                var project = await _iproject!.DeleteProject(id);
                if (project == "Deleted Successfully")
                {
                    return Ok("Deleted");
                }
                if (project == "Project not found")
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

        [HttpGet("get/{id}")]
        public async Task<ActionResult> GetProject([FromRoute] string id)
        {
            try
            {
                var project = await _iproject!.GetProject(id);
                if (project == null)
                {
                    return NotFound();
                }
                return Ok(project);
            }
            catch (System.Exception)
            {
                return null!;
            }
        }
    }
}