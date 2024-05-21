namespace Messenger.Application.ViewModels
{
    public class MessageViewModel
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }

        public UserViewModel? Sender { get; set; }
        public MessageViewModel? Parent { get; set; }
    }
}
