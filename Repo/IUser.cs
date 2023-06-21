using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Backend.DTO;

namespace Backend.Repo
{
    public interface IUser
    {
        public Task<IEnumerable<User>> GetUsers();
        public Task<User> GetUser(string id);
        public Task<string> UpdateUser(string id, User_DTO user);
        public Task<string> DeleteUser(string id);
        public Task<string> CreateUser(User_DTO user);
    }
}