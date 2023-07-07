using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Backend.Collective;
using Backend.DTO;
using Backend.Models;
using Backend.Repo;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class LoginServices : ILogin
    {
        private readonly LE_dbContext? _cbtContext;
        private readonly IConfiguration? _config;
        public LoginServices(LE_dbContext? cbtContext, IConfiguration? config)
        {
            _cbtContext = cbtContext;
            _config = config;
        }

        ////////////////////////////////////For Users////////////////////////////////////

        public async Task<Authenticate_DTO> Authenticate(string email, string password)
        {
            try
            {
                var user = await _cbtContext!.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
                if (user == null)
                {
                    return null!;
                }

                if (BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                {
                    var authUser = new Authenticate_DTO
                    {
                        Email = user.Email,
                        Id = user.Id,
                        // FirstName = user.FirstName,
                        // LastName = user.LastName,
                        // PhoneNumber = user.PhoneNumber,
                        Role = user.Role
                    };

                    return authUser;
                }
            }
            catch (DbException ex)
            {
                // Log the exception or handle it appropriately.
                // Return an error response if necessary.
                Console.WriteLine(ex.Message);
            }

            return null!;
        }


        public async Task<Authenticate_DTO> Login(string email, string password)
        {
            try
            {
                var user = await Authenticate(email, password);
                if (user == null)
                {
                    return null!;
                }
                var genToken = new GenToken(_config);
                user.Token = genToken.generate_Token(user);
                return user;
            }
            catch (System.Exception)
            {
                return null!;
            }
        }
    }
}