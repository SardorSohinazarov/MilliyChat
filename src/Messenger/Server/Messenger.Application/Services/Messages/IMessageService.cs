using Messenger.Application.DataTransferObjects.Messages;
using Messenger.Application.Models;
using Messenger.Application.ViewModels;

namespace Messenger.Application.Services.Messages
{
    public interface IMessageService
    {
        ValueTask<MessageViewModel> CreateMessageAsync(MessageCreationDTO messageCreationDTO);
        IQueryable<MessageViewModel> RetrieveMessages(QueryParameter queryParameter);
        ValueTask<MessageViewModel> RetrieveMessageByIdAsync(Guid messageId);
        ValueTask<MessageViewModel> ModifyMessageAsync(MessageModificationDTO messageModificationDTO);
        ValueTask<MessageViewModel> RemoveMessageAsync(Guid messageId);
    }
}
