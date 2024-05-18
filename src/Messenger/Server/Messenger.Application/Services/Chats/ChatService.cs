using Messenger.Application.DataTransferObjects.Chats;
using Messenger.Application.Models;
using Messenger.Application.ViewModels;

namespace Messenger.Application.Services.Chats
{
    public class ChatService : IChatService
    {
        public ValueTask<ChatViewModel> CreateChatAsync(ChatCreationDTO chatCreationDTO)
        {
            throw new NotImplementedException();
        }

        public ValueTask<ChatViewModel> ModifyChatAsync(ChatModificationDTO chatModificationDTO)
        {
            throw new NotImplementedException();
        }

        public ValueTask<ChatViewModel> RemoveChatAsync(Guid ChatId)
        {
            throw new NotImplementedException();
        }

        public ValueTask<ChatViewModel> RetrieveChatByIdAsync(Guid ChatId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ChatViewModel> RetrieveChats(QueryParameter queryParameter)
        {
            throw new NotImplementedException();
        }
    }
}
