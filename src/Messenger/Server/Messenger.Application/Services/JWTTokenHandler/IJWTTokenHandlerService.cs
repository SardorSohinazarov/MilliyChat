using Messenger.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace Messenger.Application.Services.JWTTokenHandler
{
    public interface IJWTTokenHandlerService
    {
        JwtSecurityToken GenerateAccessToken(User user);
        string GenerateRefreshToken();
    }
}
