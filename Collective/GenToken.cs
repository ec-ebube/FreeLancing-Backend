using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Backend.DTO;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Backend.Collective
{
    public class GenToken
    {
        private readonly IConfiguration _config;
        public GenToken(IConfiguration? config)
        {
            _config = config!;
        }

        public string generate_Token(Authenticate_DTO authenticate)
        {
            // Get the JWT key from configuration and create a security key
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config!.GetSection("Jwt:Key").Value!));

            // Create signing credentials using the security key
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Create an array of claims representing the user's identity
            var Claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, authenticate!.Id!.ToString()!),
                // new Claim(ClaimTypes.Name, authenticate!.FirstName!),
                // new Claim(ClaimTypes.GivenName, authenticate!.LastName!),
                new Claim(ClaimTypes.Role, authenticate!.Role!),
                new Claim(ClaimTypes.Email, authenticate!.Email!),
            };

            // Create a token descriptor that specifies the token properties
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(Claims),
                Expires = DateTime.Now.AddDays(30),
                SigningCredentials = credentials,
                Issuer = _config.GetSection("Jwt:Issuer").Value,
                Audience = _config.GetSection("Jwt:Audience").Value
            };

            // Create a new instance of JwtSecurityTokenHandler
            var tokenHandler = new JwtSecurityTokenHandler();

            // Generate the token based on the token descriptor
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Create an anonymous object to hold the token and its expiration date
            var mainToken = new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            };

            // Serialize the mainToken object to JSON format
            return JsonConvert.SerializeObject(mainToken);
        }

    }
}