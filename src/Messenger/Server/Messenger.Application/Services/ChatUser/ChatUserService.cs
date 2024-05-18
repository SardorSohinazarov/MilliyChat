using Messenger.Application.DataTransferObjects.ChatUsers;
using Messenger.Application.Models;
using Messenger.Application.ViewModels;

namespace Messenger.Application.Services.ChatUser
{
    public class ChatUserService : IChatUserService
    {
        public ValueTask<ChatUserViewModel> CreateChatUserAsync(ChatUserCreationDTO chatUserCreationDTO)
        {
            throw new NotImplementedException();
        }

        public ValueTask<ChatUserViewModel> ModifyChatUserAsync(ChatUserModificationDTO chatUserModificationDTO)
        {
            throw new NotImplementedException();
        }

        public ValueTask<ChatUserViewModel> RemoveChatUserAsync(Guid ChatUserId)
        {
            throw new NotImplementedException();
        }

        public ValueTask<ChatUserViewModel> RetrieveChatUserByIdAsync(Guid ChatUserId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ChatUserViewModel> RetrieveChatUsers(QueryParameter queryParameter)
        {
            throw new NotImplementedException();
        }
    }
}
