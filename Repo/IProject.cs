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
        public Task<IEnumerable<Project>> GetActivities();
        public Task<Project> GetActivity(string id);
        public Task<string> CreateActivity(Project_DTO project);
        public Task<string> UpdateActivity(string id, Project_DTO project);
        public Task<string> DeleteActivity(string id);
    }
}