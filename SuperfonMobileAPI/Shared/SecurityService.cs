using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SuperfonMobileAPI.Services
{
    public class SecurityService
    {
        private string jwtKey;
        private string jwtIssuer;
        private string jwtAudience;
        public SecurityService(IConfiguration configuration)
        {
            jwtKey = configuration["Security:jwtKey"];
            jwtIssuer = configuration["Security:jwtIssuer"];
            jwtAudience = configuration["Security:jwtAudience"];
        }

        public string GenerateToken(int userId)
        {
            var claims = new Claim[]
            {
               new Claim("UserId",userId.ToString())
            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(jwtIssuer, jwtAudience, claims,
              expires: DateTime.UtcNow.AddDays(1),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string ComputeSha256Hash(string rawData)
        {
            using SHA256 sha256Hash = SHA256.Create();
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();

        }
    }
}
