using Messenger.Application.ViewModels;
using Messenger.Domain.Entities;

namespace Messenger.Application.Mappers
{
    public static class ChatMapper
    {
        public static ChatViewModel ToChatViewModel(this Chat chat)
        {
            return new ChatViewModel()
            {
                Id = chat.Id,
                Type = chat.Type,
                Title = chat.Title,
                Link = chat.Link,
                MembersCount = chat.Users.Count,
                OwnerId = chat.OwnerId,
                CreatedAt = chat.CreatedAt,
            };
        }
    }

}
