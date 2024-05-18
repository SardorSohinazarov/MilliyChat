using Messenger.Application.DataTransferObjects.Chats;
using Messenger.Application.Models;
using Messenger.Application.ViewModels;

namespace Messenger.Application.Services.Chats
{
    public interface IChatService
    {
        ValueTask<ChatViewModel> CreateChatAsync(ChatCreationDTO chatCreationDTO);
        IQueryable<ChatViewModel> RetrieveChats(QueryParameter queryParameter);
        ValueTask<ChatViewModel> RetrieveChatByIdAsync(Guid ChatId);
        ValueTask<ChatViewModel> ModifyChatAsync(ChatModificationDTO chatModificationDTO);
        ValueTask<ChatViewModel> RemoveChatAsync(Guid ChatId);
    }
}
