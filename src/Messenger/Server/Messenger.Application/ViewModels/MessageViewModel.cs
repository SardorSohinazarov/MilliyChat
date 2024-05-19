using Messenger.Domain.Entities;

namespace Messenger.Application.ViewModels
{
    public class MessageViewModel
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }

        public User Sender { get; set; }
        public Message Parent { get; set; }
    }
}
