using Messenger.Application.DataTransferObjects.Authentication;
using Messenger.Application.Services.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
            => _authenticationService = authenticationService;

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
            => Ok(await _authenticationService.LoginAsync(loginDTO));

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterDTO registerDTO)
            => Ok(await _authenticationService.RegisterAsync(registerDTO));

        [HttpPost]
        public async Task<IActionResult> RefreshTokenAsync(RefreshTokenDTO refreshTokenDTO)
            => Ok(await _authenticationService.RefreshTokenAsync(refreshTokenDTO));
    }
}
