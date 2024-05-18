using Messenger.Application.DataTransferObjects.Authentication;

namespace Messenger.Application.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<TokenDTO> LoginAsync(LoginDTO loginDTO);
        Task<TokenDTO> RegisterAsync(RegisterDTO registerDTO);
        Task<TokenDTO> RefreshTokenAsync(RefreshTokenDTO refreshTokenDTO);
    }
}
