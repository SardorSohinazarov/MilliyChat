using Messenger.Application.DataTransferObjects.Chats;
using Messenger.Application.Models;
using Messenger.Application.ViewModels;

namespace Messenger.Application.Services.Chats
{
    public interface IChatService
    {
        ValueTask<Guid> CreatePersonalChatAsync(PersonalChatCreationDTO personalChatCreationDTO);
        ValueTask<Guid> CreateGroupChatAsync(GroupChatCreationDTO groupChatCreationDTO);
        ValueTask<Guid> CreateChannelChatAsync(ChannelChatCreationDTO channelChatCreationDTO);
        List<ChatViewModel> RetrieveChats(QueryParameter queryParameter);
        List<ChatViewModel> RetrieveActiveChats(QueryParameter queryParameter);
        List<ChatViewModel> RetrieveUserChats(QueryParameter queryParameter);
        List<ChatViewModel> RetrieveUserActiveChats(QueryParameter queryParameter);
        ValueTask<ChatViewModel> RetrieveChatByIdAsync(Guid ChatId);
        ValueTask<ChatViewModel> ModifyChatAsync(ChatModificationDTO chatModificationDTO);
        ValueTask<ChatViewModel> RemoveChatAsync(Guid ChatId);
        ValueTask<ChatViewModel> ClearChatMessagesAsync(Guid chatId);
        ValueTask<Guid> GetOrCreateAsync(long userId);
    }
}
