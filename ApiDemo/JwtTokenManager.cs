using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using ApiDemo.Interfaces;
using ApiDemo.Model;
using ApiDemo.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ApiDemo.Repository
{
    public class JwtTokenManager:IJwtTokenManager
    {
        private readonly IConfiguration _configuration;
        private readonly UserContext _userContext;
        public JwtTokenManager(IConfiguration configuration, UserContext userContext)
        {
            _configuration = configuration;
            _userContext = userContext;
        }

        public Response Authenticate(string userName, string password)
        {
            var users = _userContext.Users.ToList();
            if (!users.Any(x => x.EmailId.Equals(userName) && x.Password.Equals(password)))
                return null;
            List<User> userList = users.Where(x => x.EmailId.Equals(userName) && x.Password.Equals(password)).ToList();
            var key = _configuration.GetValue<string>("JwtConfig:Key");
            var keyBytes = Encoding.ASCII.GetBytes(key);

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]{
                new Claim(ClaimTypes.Name, userName)
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes),SecurityAlgorithms.HmacSha256Signature)

            };
            Response response = new Response();
            response.Id = userList[0].Id;
            var token = tokenHandler.CreateToken(tokenDescriptor);
            response.Token = tokenHandler.WriteToken(token);
            var expires = tokenDescriptor.Expires.GetValueOrDefault();
            DateTime dateTime = DateTime.Now;
            var rem= expires - dateTime;
            response.ExpTime = (long)rem.TotalMilliseconds;
            return response;
        }
    }
}
