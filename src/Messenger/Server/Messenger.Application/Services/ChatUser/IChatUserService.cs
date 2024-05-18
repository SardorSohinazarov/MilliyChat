using Messenger.Application.DataTransferObjects.ChatUsers;
using Messenger.Application.Models;
using Messenger.Application.ViewModels;

namespace Messenger.Application.Services.ChatUser
{
    public interface IChatUserService
    {
        ValueTask<ChatUserViewModel> CreateChatUserAsync(ChatUserCreationDTO chatUserCreationDTO);
        IQueryable<ChatUserViewModel> RetrieveChatUsers(QueryParameter queryParameter);
        ValueTask<ChatUserViewModel> RetrieveChatUserByIdAsync(Guid ChatUserId);
        ValueTask<ChatUserViewModel> ModifyChatUserAsync(ChatUserModificationDTO chatUserModificationDTO);
        ValueTask<ChatUserViewModel> RemoveChatUserAsync(Guid ChatUserId);
    }
}
