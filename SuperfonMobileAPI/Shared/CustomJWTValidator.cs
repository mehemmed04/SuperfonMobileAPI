using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;

namespace SuperfonMobileAPI.Shared
{
    public class CustomJWTValidator
    {
        private string jwtKey;
        private string jwtIssuer;
        private string jwtAudience;
        private List<string> savedTokens = new List<string>();
        public CustomJWTValidator(IConfiguration configuration)
        {
            jwtKey = configuration["Security:jwtKey"];
            jwtIssuer = configuration["Security:jwtIssuer"];
            jwtAudience = configuration["Security:jwtAudience"];
        }

        public bool ExistsInCache(string token) => savedTokens.Contains(token);
        public void ClearAll() => savedTokens.Clear();
        public void AddToken(string token) => savedTokens.Add(token);
        public int ValidateToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return 0;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtKey);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value);

                // return user id from JWT token if validation successful
                return userId;
            }
            catch
            {
                // return null if validation fails
                return 0;
            }
        }
    }
}
