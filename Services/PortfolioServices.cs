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
    public class PortfolioServices : IPortfolio
    {
        private readonly LE_dbContext _leContext;
        public PortfolioServices(LE_dbContext leContext)
        {
            _leContext = leContext;
        }

        ///////////////////////////////////////////////////////////////
        public async Task<bool> IsEmailExists(string Email)
        {
            var IsSeen = await _leContext!.Users.FirstOrDefaultAsync(e => e.Email == Email);
            return IsSeen != null!;
        }
        public async Task<bool> UsernameExist(string UserName)
        {
            var UserSeen = await _leContext!.Users.FirstOrDefaultAsync(e => e.UserName == UserName);
            return UserSeen != null!;
        }


        public async Task<string> CreatePortfolio(Portfolio_DTO portfolio)
        {
            var newPF = new Portfolio();
            if (await IsEmailExists(portfolio.Email!))
            {
                string EmailExist = "Email is already in use";
                return (EmailExist);
            }
            else
            {
                newPF.Email = portfolio.Email;
            }
            newPF.FirstName = portfolio.FirstName;
            newPF.LastName = portfolio.LastName;
            newPF.Bio = portfolio.Bio;
            newPF.Skill = portfolio.Skill;
            if (await UsernameExist(portfolio.UserName!))
            {
                string UserExists = "This UserName is already in Use";
                return (UserExists);
            }
            else
            {
                newPF.UserName = portfolio.UserName;
            }
            newPF.ProfilePhoto = ;

            var res = await _leContext!.Portfolios.AddAsync(newPF);
            if (res == null)
            {
                throw new Exception("something went wrong");
            }
            _leContext!.SaveChanges();
            return "Portfolio Created Successfuly";
        }

        public Task<string> DeletePortfolio(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Portfolio> GetPortfolio(string Username)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Portfolio>> GetPortfolios()
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdatePortfolio(string id, Portfolio_DTO portfolio)
        {
            throw new NotImplementedException();
        }
    }
}