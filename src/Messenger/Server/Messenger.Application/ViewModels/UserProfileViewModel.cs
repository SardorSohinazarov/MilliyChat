using Messenger.Domain.Entities;

namespace Messenger.Application.ViewModels
{
    public class UserProfileViewModel
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string PhoneNumber { get; set; }
        public string? LastName { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? PhotoPath { get; set; }
        public List<Chat> AuthorshipChats { get; set; }
        public List<Chat> Chats { get; set; }
        public List<Message> Messages { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
