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
        public Task<string> CreateActivity(Project_DTO project)
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteActivity(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Project>> GetActivities()
        {
            throw new NotImplementedException();
        }

        public Task<Project> GetActivity(string id)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateActivity(string id, Project_DTO project)
        {
            throw new NotImplementedException();
        }
    }
}