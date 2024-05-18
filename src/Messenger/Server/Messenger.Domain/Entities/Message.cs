using Messenger.Domain.Common;

namespace Messenger.Domain.Entities
{
    public class Message : Auditable<Guid>
    {
        public string Text { get; set; }
        public Guid? ParentId { get; set; }
        public long SenderId { get; set; }
        public Guid ChatId { get; set; }

        public virtual Chat Chat { get; set; }
        public virtual User Sender { get; set; }
        public virtual Message Parent { get; set; }
        public virtual List<Message> Childrens { get; set; }
    }
}
