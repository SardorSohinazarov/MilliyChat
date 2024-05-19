using Messenger.Application.Services.ChatUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.API.Controllers
{
    [Route("api/connections")]
    [ApiController]
    [Authorize]
    public class ChatUsersController : ControllerBase
    {
        private readonly IChatUserService _chatUserService;

        public ChatUsersController(IChatUserService chatUserService)
            => _chatUserService = chatUserService;

        [HttpPost("join-chat/{chatId}")]
        public async Task<IActionResult> PostChatUserAsync(Guid chatId)
            => Ok();

        [HttpPost("add-member")]
        public async Task<IActionResult> PostChatUserAsync()
            => Ok();

        [HttpPost("block-chat-member")]
        public async Task<IActionResult> UpdateChatUserAsync()
          => Ok();

        [HttpPost("block-chat/{chatId}")]
        public async Task<IActionResult> UpdateChatAsync(Guid chatId)
        => Ok();

        [HttpDelete("leave-chat/{chatId}")]
        public async Task<IActionResult> DeleteChatUser(Guid chatId)
            => Ok();
    }
}
