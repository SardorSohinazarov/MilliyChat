using Messenger.Application.Models;
using Messenger.Application.Services.Users;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
            => _userService = userService;

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(long userId)
            => Ok(await _userService.RetrieveUserByIdAsync(userId));

        [HttpGet]
        public IActionResult GetUsers([FromQuery] QueryParameter parameter)
            => Ok(_userService.RetrieveUsers(parameter));

        [HttpGet("chat/{chatId}")]
        public IActionResult GetUsers([FromQuery] QueryParameter parameter, Guid chatId)
            => Ok(_userService.RetrieveByChatIdUsers(parameter, chatId));

        [HttpPut]
        public async Task<IActionResult> UpdateUserAsync(long userId)
            => Ok();
    }
}
