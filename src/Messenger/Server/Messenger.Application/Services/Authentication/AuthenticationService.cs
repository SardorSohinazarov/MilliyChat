using Messenger.Application.DataTransferObjects.Authentication;
using Messenger.Application.Models;
using Messenger.Application.Services.JWTTokenHandler;
using Messenger.Application.Services.PasswordHasher;
using Messenger.Domain.Exceptions;
using Messenger.Infrastructure.Repositories.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Messenger.Application.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasherService _passwordHasherService;
        private readonly IJWTTokenHandlerService _jWTTokenHandlerService;
        private readonly JWTOption _jWTOptions;

        public AuthenticationService(
            IUserRepository userRepository,
            IPasswordHasherService passwordHasherService,
            IJWTTokenHandlerService jWTTokenHandlerService,
            IOptions<JWTOption> jWTOptions)
        {
            _userRepository = userRepository;
            _passwordHasherService = passwordHasherService;
            _jWTTokenHandlerService = jWTTokenHandlerService;
            _jWTOptions = jWTOptions.Value;
        }

        public async Task<TokenDTO> LoginAsync(LoginDTO loginDTO)
        {
            var user = await _userRepository
                .SelectByIdWithDetailsAsync(
                    expression: user => user.PhoneNumber == loginDTO.PhoneNumber,
                    includes: Array.Empty<string>());

            if (user is null)
            {
                throw new NotFoundException("User with given credentials not found");
            }

            if (!_passwordHasherService.Verify(
                hash: user.PasswordHash,
                password: loginDTO.Password,
                salt: user.Salt))
            {
                throw new ValidationException("Username or password is not valid");
            }

            string refreshToken = _jWTTokenHandlerService
                .GenerateRefreshToken();

            user.UpdateRefreshToken(refreshToken);

            var updatedUser = await _userRepository
                .UpdateAsync(user);

            var accessToken = _jWTTokenHandlerService
                .GenerateAccessToken(updatedUser);

            return new TokenDTO(
                AccessToken: new JwtSecurityTokenHandler().WriteToken(accessToken),
                RefreshToken: user.RefreshToken,
                ExpireDate: accessToken.ValidTo);
        }

        public async Task<TokenDTO> RefreshTokenAsync(RefreshTokenDTO refreshTokenDto)
        {
            var claimsPrincipal = GetPrincipalFromExpiredToken(refreshTokenDto.AccessToken);

            var userId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier).Value;

            var storageUser = await _userRepository
                .SelectByIdAsync(long.Parse(userId));

            if (!storageUser.RefreshToken.Equals(refreshTokenDto.RefreshToken))
            {
                throw new ValidationException("Refresh token is not valid");
            }

            if (storageUser.RefreshTokenExpireDate <= DateTime.UtcNow)
            {
                throw new ValidationException("Refresh token has already been expired");
            }

            var newAccessToken = _jWTTokenHandlerService
                .GenerateAccessToken(storageUser);

            return new TokenDTO(
                AccessToken: new JwtSecurityTokenHandler()
                    .WriteToken(newAccessToken),

                RefreshToken: storageUser.RefreshToken,
                ExpireDate: newAccessToken.ValidTo);
        }

        public async Task<TokenDTO> RegisterAsync(RegisterDTO registerDTO)
        {
            var user = await _userRepository
                .SelectByIdWithDetailsAsync(
                    expression: user => user.PhoneNumber == registerDTO.PhoneNumber,
                    includes: Array.Empty<string>());

            if (user is not null)
            {
                throw new Exception("This phone number already registred");
            }

            var salt = Guid.NewGuid().ToString("N");
            var passwordHash = _passwordHasherService.Encrypt(registerDTO.Password, salt);

            //mapping
            user.FirstName = registerDTO.FirstName;
            user.PhoneNumber = registerDTO.PhoneNumber;
            user.LastName = registerDTO.LastName;
            user.Email = registerDTO.Email;
            user.Username = registerDTO.Username;
            user.PasswordHash = passwordHash;

            user = await _userRepository.InsertAsync(user);

            var accessToken = _jWTTokenHandlerService
                .GenerateAccessToken(user);

            return new TokenDTO(
                AccessToken: new JwtSecurityTokenHandler().WriteToken(accessToken),
                RefreshToken: user.RefreshToken,
                ExpireDate: accessToken.ValidTo);
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(
       string accessToken)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidAudience = _jWTOptions.Audience,
                ValidateIssuer = true,
                ValidIssuer = _jWTOptions.Issuer,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = false,

                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jWTOptions.SecretKey))
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var principal = tokenHandler.ValidateToken(
                token: accessToken,
                validationParameters: tokenValidationParameters,
                validatedToken: out SecurityToken securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(
                SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new ValidationException("Invalid token");
            }

            return principal;
        }
    }
}
