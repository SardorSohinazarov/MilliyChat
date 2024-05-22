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
        public List<ChatViewModel> AuthorshipChats { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
