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
                MembersCount = chat.Users is null ? 0 : chat.Users.Count,
                OwnerId = chat.OwnerId,
                CreatedAt = chat.CreatedAt,
            };
        }

        public static MessageViewModel ToMessageViewModel(this Message message)
        {
            return new MessageViewModel()
            {
                Id = message.Id,
                Text = message.Text,
                Parent = message.Parent,
                Sender = message.Sender
            };
        }
    }

}
