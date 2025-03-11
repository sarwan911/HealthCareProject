using HealthCareProject.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using HealthCareProject.Authentication;
using HealthCareProject.Data;

namespace WebapiProject.Authentication
{
    public class Auth : IAuth
    {
        private readonly string _key;
        private readonly Context _applicationDbContext;
        public Auth(string key, Context applicationDbContext)
        {
            _key = key;
            _applicationDbContext = applicationDbContext;
        }
        public string GenerateToken(User user)
        {
            // 1. Create Security Token Handler
            var tokenHandler = new JwtSecurityTokenHandler();
            // 2. Create Private Key for Encryption
            var tokenKey = Encoding.ASCII.GetBytes(_key);
            // 3. Create JWT Descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                   new Claim(ClaimTypes.Name, user.Name),
                   new Claim(ClaimTypes.Email, user.Email),
                   new Claim(ClaimTypes.Role, user.Role),
               }),
                Expires = DateTime.UtcNow.AddHours(1), // Token Expiration
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            // 4. Create Token
            var token = tokenHandler.CreateToken(tokenDescriptor);
            // 5. Return Token from Method
            return tokenHandler.WriteToken(token);
        }
    }
}