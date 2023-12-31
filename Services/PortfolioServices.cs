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
            var IsSeen = await _leContext!.Portfolios.FirstOrDefaultAsync(e => e.Email == Email);
            return IsSeen != null!;
        }
        public async Task<bool> UsernameExist(string UserName)
        {
            var UserSeen = await _leContext!.Portfolios.FirstOrDefaultAsync(e => e.UserName == UserName);
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
            newPF.PasswordHash = BCrypt.Net.BCrypt.HashPassword(portfolio.Password);
            newPF.DoB = portfolio.DoB;
            newPF.Category = portfolio.Category;
            newPF.Pricing = portfolio.Pricing;
            if (await UsernameExist(portfolio.UserName!))
            {
                string UserExists = "This UserName is already in Use";
                return (UserExists);
            }
            else
            {
                newPF.UserName = portfolio.UserName;
            }


            if (portfolio.ProfilePhoto != null && portfolio.ProfilePhoto.Length > 0)
            {
                var uniq = Guid.NewGuid();
                var filepath = Path.Combine("wwwroot/Images/", uniq + ".jpg");
                var fileStream = new FileStream(filepath, FileMode.Create);
                portfolio.ProfilePhoto.CopyTo(fileStream);
                newPF.ProfilePath = filepath;
            }
            newPF.Created = DateTime.Now;
            newPF.Modified = DateTime.Now;

            var res = await _leContext!.Portfolios.AddAsync(newPF);
            // if (res == null)
            // {
            //     throw new Exception("something went wrong");
            // }
            _leContext!.SaveChanges();
            return "Portfolio Created Successfuly";
        }

        public async Task<string> DeletePortfolio(string id)
        {
            try
            {
                var portfolio = await _leContext!.Portfolios.FindAsync(id);
                if (portfolio == null)
                {
                    return "Portfolio not found";
                }
                _leContext!.Remove(portfolio);
                _leContext!.SaveChanges();
                return "Deleted";
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<Portfolio> GetPortfolio(string Username)
        {
            try
            {
                var portfolio = await _leContext!.Portfolios.FirstOrDefaultAsync(p => p.UserName == Username);
                if (portfolio == null)
                {
                    return null!;
                }
                // var dPortfolio = _leContext!.Portfolios.FirstOrDefault();
                return portfolio!;
            }
            catch (System.Exception)
            {
                return null!;
            }
        }

        public async Task<Portfolio> GetAPortfolio(string Id)
        {
            try
            {
                var portfolio = await _leContext!.Portfolios.FirstOrDefaultAsync(p => p.Id == Id);
                if (portfolio == null)
                {
                    return null!;
                }
                return portfolio!;
            }
            catch (System.Exception)
            {
                return null!;
            }
        }

        public async Task<IEnumerable<Portfolio>> GetPortfolios()
        {
            try
            {
                var portfolios = await _leContext!.Portfolios.OrderBy(x => x.UserName).ToListAsync();
                if (portfolios.Count == 0)
                {
                    return null!;
                }
                return portfolios;
            }
            catch (System.Exception)
            {
                return null!;
            }
        }

        public async Task<string> UpdatePortfolio(string id, Portfolio_DTO portfolio)
        {
            try
            {
                var editPF = await _leContext!.Portfolios.FindAsync(id);
                var uniq = Guid.NewGuid();
                var filepath = Path.Combine("wwwroot/Images/", uniq + ".jpg");
                var fileStream = new FileStream(filepath, FileMode.Create);
                portfolio.ProfilePhoto!.CopyTo(fileStream);
                editPF!.ProfilePath = filepath;

                editPF.FirstName = portfolio.FirstName;
                editPF.LastName = portfolio.LastName;
                editPF.Bio = portfolio.Bio;
                editPF.Skill = portfolio.Skill;
                editPF.DoB = portfolio.DoB;
                editPF.Category = portfolio.Category;
                editPF.Pricing = portfolio.Pricing;
                if (await IsEmailExists(portfolio.Email!))
                {
                    string EmailExist = "Email is already in use";
                    return (EmailExist);
                }
                else
                {
                    editPF.Email = portfolio.Email;
                }
                if (await UsernameExist(portfolio.UserName!))
                {
                    string EmailExist = "Username is already in use";
                    return (EmailExist);
                }
                else
                {
                    editPF.UserName = portfolio.UserName;
                }
                editPF.Modified = DateTime.Now;

                _leContext!.Portfolios.Attach(editPF);
                _leContext!.SaveChanges();
                return "Updated Successfully";

            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }

        }
    }
}