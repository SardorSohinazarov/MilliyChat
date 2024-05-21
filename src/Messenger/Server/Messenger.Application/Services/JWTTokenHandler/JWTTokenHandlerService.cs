using Messenger.Application.Models;
using Messenger.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Messenger.Application.Services.JWTTokenHandler
{
    public class JWTTokenHandlerService : IJWTTokenHandlerService
    {
        private readonly JWTOption _jwtOption;

        public JWTTokenHandlerService(IConfiguration configuration)
            => _jwtOption = configuration.GetSection("JwtSettings").Get<JWTOption>();

        public JwtSecurityToken GenerateAccessToken(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                new Claim(ClaimTypes.Name, user.FirstName),
            };

            var authSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jwtOption.SecretKey));

            var token = new JwtSecurityToken(
                issuer: _jwtOption.Issuer,
                audience: _jwtOption.Audience,
                expires: DateTime.UtcNow.AddMinutes(_jwtOption.ExpirationInMinutes),
                claims: claims,
                signingCredentials: new SigningCredentials(
                    key: authSigningKey,
                    algorithm: SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        public string GenerateRefreshToken()
        {
            byte[] bytes = new byte[64];

            using var randomGenerator = RandomNumberGenerator.Create();
            randomGenerator.GetBytes(bytes);

            return Convert.ToBase64String(bytes);
        }
    }
}
