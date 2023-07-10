using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTO;

namespace Backend.Repo
{
    public interface ILogin
    {
         public Task<Authenticate_DTO> Login(string email, string password);
         public Task<Authenticate_DTO> LogP(string email, string password);
        public Task<Authenticate_DTO> Authenticate(string email, string password);
    }
}