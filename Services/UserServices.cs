using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTO;
using Backend.Models;
using Backend.Repo;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class UserServices : IUser
    {
        private readonly LE_dbContext? _leContext;
        public UserServices(LE_dbContext leContext)
        {
            _leContext = leContext;
        }

        // //////////////////////////////////////////////////////////////////
        public async Task<bool> IsEmailExists(string Email)
        {
            var IsSeen = await _leContext!.Users.FirstOrDefaultAsync(e => e.Email == Email);
            return IsSeen != null!;
        }

        public async Task<string> CreateUser(User_DTO user)
        {
            try
            {
                var newUser = new User();
                if (await IsEmailExists(user.Email!))
                {
                    string EmailExist = "Email is already in use";
                    return (EmailExist);
                }
                else
                {
                    newUser.Email = user.Email;
                }
                newUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
                if (newUser.Role == null)
                {
                    newUser.Role = "User";
                }
                else
                {
                    newUser.Role = user.Role;
                }
                newUser.Created_at = DateTime.Now;
                newUser.Updated_at = DateTime.Now;

                var res = await _leContext!.Users.AddAsync(newUser);
                if (res == null)
                {
                    throw new Exception("something went wrong");
                }
                Console.WriteLine(res);
                await _leContext.SaveChangesAsync();
                return "User Created Successfully";
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> DeleteUser(string id)
        {
            try
            {
                var user = await _leContext!.Users.FindAsync(id);
                if (user == null)
                {
                    throw new Exception("User Not Found");
                }
                _leContext!.Remove(user);
                _leContext.SaveChanges();
                return "User Deleted";
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<User> GetUser(string id)
        {
            try
            {
                var user = _leContext!.Users.Where(u => u.Id == id);
                if (user == null)
                {
                    return null!;
                }
                var dUser = await _leContext!.Users.FirstOrDefaultAsync();

                return dUser!;
            }
            catch (System.Exception)
            {
                return null!;
            }
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            try
            {
                var users = await _leContext!.Users.OrderByDescending(x => x.Created_at).ToListAsync();
                if (users.Count == 0)
                {
                    return null!;
                }
                return users;
            }
            catch (System.Exception)
            {
                return null!;
            }
        }

        public async Task<string> UpdateUser(string id, User_DTO user)
        {
            try
            {
                var editUser = await _leContext!.Users.FindAsync(id);
                if (editUser == null)
                {
                    return null!;
                }
                if (await IsEmailExists(user.Email!))
                {
                    string EmailExist = "Email is already in use";
                    return (EmailExist);
                }
                else
                {
                    editUser.Email = user.Email;
                }
                editUser.Updated_at = DateTime.Now;
                _leContext.Users.Attach(editUser);
                _leContext.SaveChanges();
                return "Edited Successfully";
            }
            catch (System.Exception ex)
            {
                return (ex.Message);
            }
        }
    }
}