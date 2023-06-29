using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTO;
using Backend.Models;

namespace Backend.Repo
{
    public interface IProject
    {
        public Task<IEnumerable<Project>> GetProjects();
        public Task<Project> GetProject(string id);
        public Task<string> CreateProject(Project_DTO project);
        public Task<string> UpdateProject(string id, Project_DTO project);
        public Task<string> DeleteProject(string id);
    }
}