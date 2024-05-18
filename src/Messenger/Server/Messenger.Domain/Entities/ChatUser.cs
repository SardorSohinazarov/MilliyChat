using Messenger.Domain.Common;

namespace Messenger.Domain.Entities
{
    public class ChatUser : Auditable<Guid>
    {
        public bool IsBlocked { get; set; }

        public long UserId { get; set; }
        public Guid ChatId { get; set; }

        public virtual User User { get; set; }
        public virtual Chat Chat { get; set; }
    }
}
