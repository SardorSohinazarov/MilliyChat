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
                MembersCount = chat.Users is null ? 0 : chat.Users.Select(x => x.User).ToList().Count,
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

        public static UserViewModel ToUserViewModel(this User user)
        {
            return new UserViewModel()
            {
                Id = user.Id,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Email = user.Email,
                PhotoPath = user.PhotoPath,
            };
        }

        public static UserProfileViewModel ToUserProfileViewModel(this User user)
        {
            return new UserProfileViewModel()
            {
                Id = user.Id,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Email = user.Email,
                PhotoPath = user.PhotoPath,
                CreatedAt = user.CreatedAt,

                Messages = user.Messages,
                Chats = user.Chats.Select(x => x.Chat).ToList(),
                AuthorshipChats = user.AuthorshipChats,
            };
        }
    }
}
