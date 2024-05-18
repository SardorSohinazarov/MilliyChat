using Messenger.Application.DataTransferObjects.Messages;
using Messenger.Application.Models;
using Messenger.Application.ViewModels;

namespace Messenger.Application.Services.Messages
{
    public class MessageService : IMessageService
    {
        public ValueTask<MessageViewModel> CreateMessageAsync(MessageCreationDTO messageCreationDTO)
        {
            throw new NotImplementedException();
        }

        public ValueTask<MessageViewModel> ModifyMessageAsync(MessageModificationDTO messageModificationDTO)
        {
            throw new NotImplementedException();
        }

        public ValueTask<MessageViewModel> RemoveMessageAsync(Guid messageId)
        {
            throw new NotImplementedException();
        }

        public ValueTask<MessageViewModel> RetrieveMessageByIdAsync(Guid messageId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<MessageViewModel> RetrieveMessages(QueryParameter queryParameter)
        {
            throw new NotImplementedException();
        }
    }
}
