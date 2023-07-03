using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTO;
using Backend.Models;
using Backend.Repo;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace Backend.Services
{
    public class ProjectServices : IProject
    {
        private readonly LE_dbContext? _leContext;
        public ProjectServices(LE_dbContext leContext)
        {
            _leContext = leContext;
        }

        public async Task<string> CreateProject(Project_DTO project)
        {
            try
            {
                var newpj = new Project();
                var id = Guid.NewGuid();
                newpj.Id = id.ToString();
                newpj.Portfolio_Id = project.Portfolio_Id;
                newpj.Title = project.Title;
                newpj.Description = project.Description;
                if (project.ProjectImage != null && project.ProjectImage.Length > 0)
                {
                    var imgid = Guid.NewGuid();
                    var photopath = Path.Combine("wwwroot/Projects/Images/" + imgid + ".jpg");
                    var photoStream = new FileStream(photopath, FileMode.Create);
                    project.ProjectImage!.CopyTo(photoStream);
                    newpj.ProjectImagePath = photopath;
                }
                if (project.ProjectVideo != null && project.ProjectVideo.Length > 0)
                {
                    var vidid = Guid.NewGuid();
                    var vidpath = Path.Combine("wwwroot/Projects/Videos/" + vidid + ".mp4");
                    var vidStream = new FileStream(vidpath, FileMode.Create);
                    project.ProjectVideo!.CopyTo(vidStream);
                    newpj.ProjectVideoPath = vidpath;
                }
                project.Created = DateTime.Now;
                project.Modified = DateTime.Now;

                await _leContext!.projects.AddAsync(newpj);
                _leContext!.SaveChanges();
                return "Project Posted";
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> DeleteProject(string id)
        {
            try
            {
                var project = await _leContext!.projects.FindAsync(id);
                if (project == null)
                {
                    return "Project not found";
                }
                _leContext!.Remove(project);
                _leContext!.SaveChanges();
                return "Deleted Successfully";
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<IEnumerable<Project>> GetProjects()
        {
            try
            {
                var projects = await _leContext!.projects.OrderByDescending(x => x.Created).ToListAsync();
                if (projects == null)
                {
                    return null!;
                }
                return projects;
            }
            catch (System.Exception)
            {
                return null!;
            }
        }

        public async Task<Project> GetProject(string Id)
        {
            try
            {
                var project = await _leContext!.projects.FindAsync(Id);
                if (project == null)
                {
                    return null!;
                }
                return project!;
            }
            catch (System.Exception)
            {
                return null!;
            }
        }

        public async Task<string> UpdateProject(string Id, Project_DTO project)
        {
            try
            {
                var editPJ = await _leContext!.projects.FindAsync(Id);
                editPJ.Title = project.Title;
                editPJ.Description = project.Description;

                if (project.ProjectImage != null && project.ProjectImage.Length > 0)
                {
                    var imgid = Guid.NewGuid();
                    var photopath = Path.Combine("wwwroot/Projects/Images/" + imgid + ".jpg");
                    var photoStream = new FileStream(photopath, FileMode.Create);
                    project.ProjectImage!.CopyTo(photoStream);
                    editPJ.ProjectImagePath = photopath;
                    editPJ.ProjectVideoPath = null!;
                }
                if (project.ProjectVideo != null && project.ProjectVideo.Length > 0)
                {
                    var vidid = Guid.NewGuid();
                    var vidpath = Path.Combine("wwwroot/Projects/Videos/" + vidid + ".mp4");
                    var vidStream = new FileStream(vidpath, FileMode.Create);
                    project.ProjectVideo!.CopyTo(vidStream);
                    editPJ.ProjectVideoPath = vidpath;
                    editPJ.ProjectImagePath = null!;
                }
                editPJ.Modified = DateTime.Now;

                _leContext!.projects.Attach(editPJ);
                _leContext!.SaveChanges();
                
                return "Successfully Changed";
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
        }
    }
}