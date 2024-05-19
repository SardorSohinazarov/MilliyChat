using Messenger.Application.DataTransferObjects.ChatUsers;
using Messenger.Application.Models;
using Messenger.Domain.Entities;

namespace Messenger.Application.Services.ChatUsers
{
    public interface IChatUserService
    {
        List<ChatUser> GetChatUsers(QueryParameter queryParameter);
        ValueTask<ChatUser> JoinChatAsync(Guid chatId);
        ValueTask<ChatUser> AddChatMemberAsync(ChatUserCreationDTO chatUserCreationDTO);
        ValueTask<ChatUser> KickMemberAsync(ChatUserDTO chatUserDTO);
        ValueTask<ChatUser> BlockChatAsync(Guid chatId);
        ValueTask<ChatUser> BlockChatMemberAsync(ChatUserDTO chatUserDTO);
        ValueTask<ChatUser> LeaveChatAsync(Guid chatId);
    }
}
