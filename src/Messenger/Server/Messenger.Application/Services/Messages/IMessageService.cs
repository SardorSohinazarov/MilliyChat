using Messenger.Application.DataTransferObjects.Messages;
using Messenger.Application.Models;
using Messenger.Application.ViewModels;

namespace Messenger.Application.Services.Messages
{
    public interface IMessageService
    {
        ValueTask<MessageViewModel> CreateMessageAsync(MessageCreationDTO messageCreationDTO);
        List<MessageViewModel> RetrieveMessagesByUserId(QueryParameter queryParameter, long userId);
        List<MessageViewModel> RetrieveMessagesByChatId(QueryParameter queryParameter, Guid chatId);
        ValueTask<MessageViewModel> RetrieveMessageByIdAsync(Guid messageId);
        ValueTask<MessageViewModel> ModifyMessageAsync(MessageModificationDTO messageModificationDTO);
        ValueTask<MessageViewModel> RemoveMessageAsync(Guid messageId);
    }
}
