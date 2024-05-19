using Messenger.Application.DataTransferObjects.Chats;
using Messenger.Application.Models;
using Messenger.Application.Services.Chats;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.API.Controllers
{
    [Route("api/chats")]
    [ApiController]
    [Authorize]
    public class ChatsController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatsController(IChatService chatService)
            => _chatService = chatService;

        [HttpPost("create-chat")]
        public async Task<IActionResult> PostPersonalChatAsync(PersonalChatCreationDTO chatCreationDTO)
            => Ok(await _chatService.CreatePersonalChatAsync(chatCreationDTO));

        [HttpPost("create-group")]
        public async Task<IActionResult> PostGroupChatAsync(GroupChatCreationDTO chatCreationDTO)
           => Ok(await _chatService.CreateGroupChatAsync(chatCreationDTO));

        [HttpPost("create-channel")]
        public async Task<IActionResult> PostChannelChatAsync(ChannelChatCreationDTO chatCreationDTO)
           => Ok(await _chatService.CreateChannelChatAsync(chatCreationDTO));

        [HttpGet("{chatId}")]
        public async Task<IActionResult> GetChatAsync(Guid chatId)
            => Ok(await _chatService.RetrieveChatByIdAsync(chatId));

        [HttpPut]
        public async Task<IActionResult> UpdateChatAsync(ChatModificationDTO chatModificationDTO)
            => Ok(await _chatService.ModifyChatAsync(chatModificationDTO));

        [HttpDelete("{chatId}")]
        public async Task<IActionResult> DeleteAsync(Guid chatId)
            => Ok(await _chatService.RemoveChatAsync(chatId));

        [HttpGet("all-chats")]
        public IActionResult GetAllChats([FromQuery] QueryParameter paginationParam)
            => Ok(_chatService.RetrieveChats(paginationParam));

        [HttpGet("all-active-chats")]
        public IActionResult GetAllActiveChats([FromQuery] QueryParameter paginationParam)
            => Ok(_chatService.RetrieveActiveChats(paginationParam));

        [HttpGet("user-chats")]
        public IActionResult GetUserChats([FromQuery] QueryParameter paginationParam)
            => Ok(_chatService.RetrieveUserChats(paginationParam));

        [HttpGet("user-active-chats")]
        public IActionResult GetUserActiveChats([FromQuery] QueryParameter paginationParam)
            => Ok(_chatService.RetrieveUserActiveChats(paginationParam));

        [HttpDelete("{chatId}/clear-chat")]
        public async Task<IActionResult> ClearChatMessagesAsync(Guid chatId)
            => Ok();

        [HttpPost("try-create-join/{chatId}")]
        public async Task<IActionResult> GetOrCreateChatAsync(Guid chatId)
            => throw new Exception();
    }
}
