using Messenger.Application.DataTransferObjects.Messages;
using Messenger.Application.Models;
using Messenger.Application.Services.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.API.Controllers
{
    [Route("api/messages")]
    [ApiController]
    [Authorize]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessagesController(IMessageService messageService)
            => _messageService = messageService;

        [HttpPost]
        public async Task<IActionResult> PostMessageAsync(MessageCreationDTO messageCreationDTO)
            => Ok(await _messageService.CreateMessageAsync(messageCreationDTO));

        [HttpGet("user/{userId}")]
        public IActionResult GetMessages([FromQuery] QueryParameter parameter, long userId)
            => Ok(_messageService.RetrieveMessagesByUserId(parameter, userId));

        [HttpGet("chat/{chatId}")]
        public IActionResult GetMessages([FromQuery] QueryParameter parameter, Guid chatId)
            => Ok(_messageService.RetrieveMessagesByChatId(parameter, chatId));

        [HttpPut]
        public async Task<IActionResult> UpdateMessageAsync(MessageModificationDTO messageModificationDTO)
            => Ok(await _messageService.ModifyMessageAsync(messageModificationDTO));

        [HttpDelete("{messageId}")]
        public async Task<IActionResult> DeleteAsync(Guid messageId)
            => Ok(await _messageService.RemoveMessageAsync(messageId));
    }
}
