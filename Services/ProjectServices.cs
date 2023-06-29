using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTO;
using Backend.Models;
using Backend.Repo;

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
                    var photopath = Path.Combine("wwwroot/Projects/Images"+imgid+"jpg");
                    var photoStream = new FileStream(photopath, FileMode.Create);
                    project.ProjectImage!.CopyTo(photoStream);
                    newpj.ProjectImagePath = photopath;
                }
                else
                {
                    var vidid = Guid.NewGuid();
                    var vidpath = Path.Combine("wwwroot/Projects/Videos"+vidid+"mp4");
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

        public Task<string> DeleteProject(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Project>> GetProjects()
        {
            throw new NotImplementedException();
        }

        public Task<Project> GetProject(string id)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateProject(string id, Project_DTO project)
        {
            throw new NotImplementedException();
        }
    }
}