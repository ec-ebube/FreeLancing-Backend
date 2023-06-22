using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTO;
using Backend.Models;

namespace Backend.Repo
{
    public interface IPortfolio
    {
        void ProcessProfilePhoto(IFormFile profilePicture);
        public Task<IEnumerable<Portfolio>> GetPortfolios();
        public Task<Portfolio> GetPortfolio(string Username);
        public Task<string> UpdatePortfolio(string id, Portfolio_DTO portfolio);
        public Task<string> CreatePortfolio(Portfolio_DTO portfolio);
        public Task<string> DeletePortfolio(string id);
    }
}