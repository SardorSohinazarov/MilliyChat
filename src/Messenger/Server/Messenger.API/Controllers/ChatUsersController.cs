using Messenger.Application.DataTransferObjects.ChatUsers;
using Messenger.Application.Models;
using Messenger.Application.Services.ChatUsers;
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

        [HttpGet]
        public IActionResult GetChatUsers([FromQuery] QueryParameter queryParameter)
            => Ok(_chatUserService.GetChatUsers(queryParameter));

        [HttpPost("join-chat/{chatId}")]
        public async Task<IActionResult> PostChatUserAsync(Guid chatId)
            => Ok(await _chatUserService.JoinChatAsync(chatId));

        [HttpPost("add-member")]
        public async Task<IActionResult> PostChatUserAsync(ChatUserCreationDTO chatUserCreationDTO)
            => Ok(await _chatUserService.AddChatMemberAsync(chatUserCreationDTO));

        [HttpPost("kick-member")]
        public async Task<IActionResult> DeleteChatUserAsync(ChatUserDTO chatUserDTO)
            => Ok(await _chatUserService.KickMemberAsync(chatUserDTO));

        [HttpPost("block-chat-member")]
        public async Task<IActionResult> UpdateChatUserAsync(ChatUserDTO chatUserDTO)
            => Ok(await _chatUserService.BlockChatMemberAsync(chatUserDTO));

        [HttpPost("block-chat/{chatId}")]
        public async Task<IActionResult> UpdateChatUserAsync(Guid chatId)
            => Ok(await _chatUserService.BlockChatAsync(chatId));

        [HttpDelete("leave-chat/{chatId}")]
        public async Task<IActionResult> DeleteChatUserAsync(Guid chatId)
            => Ok(await _chatUserService.LeaveChatAsync(chatId));
    }
}
